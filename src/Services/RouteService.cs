using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Route;
using Entity.Route;
using Microsoft.Extensions.Configuration;
using Repositories.Interface;
using Services.Interface;
namespace Services;
[System.Diagnostics.CodeAnalysis.SuppressMessage(
    "Globalization",
    "CA1305: Especifique IFormatProvider",
    Justification = "Guid.Parse no depende de la configuración regional")]
public class RouteService(IConfiguration config, HttpClient httpClient, IRouteRepository routeRepository, IUserRepository userRepository) : IRouteService
{
  private readonly IConfiguration _config = config;
  private readonly HttpClient _httpClient = httpClient;
  private readonly IRouteRepository _routeRepository = routeRepository;
  private readonly IUserRepository _userRepository = userRepository;

  public async Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request)
  {
    if (request == null)
    {
      throw new ArgumentNullException(nameof(request), "El parámetro request no puede ser null.");
    }
    var streetNameOrigin = await GetStreetNameAsync(request.OriginLat, request.OriginLng).ConfigureAwait(false);
    var streetNameDestination = await GetStreetNameAsync(request.DestinationLat, request.DestinationLng).ConfigureAwait(false);
    var response = request.Mode == "transit"
        ? await CalcularConGoogleMapsAsync(request).ConfigureAwait(false)
        : await CalcularConORSAsync(request).ConfigureAwait(false);
    response.OriginStreetName = streetNameOrigin;
    response.DestinationStreetName = streetNameDestination;
    return response;
  }

  private async Task<RouteResponseDto> CalcularConORSAsync(RouteRequestDto request)
  {
    string profile = request.Mode switch
    {
      "car" => "driving-car",
      "hgv" => "driving-hgv",
      "bike" => "cycling-regular",
      "road-bike" => "cycling-road",
      "mountain-bike" => "cycling-mountain",
      "electric-bike" => "cycling-electric",
      "walk" => "foot-walking",
      "hiking" => "foot-hiking",
      "sustainable" => "cycling-regular", // TODO: Cambiar sustainable por funcion
      _ => "cycling-regular"
    };

    var apiKey = this._config["APIKeys:ORS:ApiKey"];
    var url = $"https://api.openrouteservice.org/v2/directions/{profile}";
    this._httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);

    var body = new
    {
      coordinates = new[]
        {
                [request.OriginLng, request.OriginLat],
                new[] { request.DestinationLng, request.DestinationLat }
            },
      preference = request.Preference switch
      {
        "shortest" => "shortest",
        _ => request.Mode == "sustainable" ? "shortest" : "recommended"
      }

    };

    using var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
    var res = await this._httpClient.PostAsync(new Uri(url), content).ConfigureAwait(false);
    var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
    Console.WriteLine("ORS response: " + json);

    using var doc = JsonDocument.Parse(json);
    if (!doc.RootElement.TryGetProperty("routes", out var routes) || routes.GetArrayLength() == 0)
    {
      throw new Exception("No se pudo calcular la ruta: respuesta de ORS inválida o vacía.");
    }

    var route = routes[0];


    if (!route.TryGetProperty("summary", out var summary))
    {
      throw new Exception("La respuesta de ORS no contiene el resumen de la ruta.");
    }


    if (!route.TryGetProperty("geometry", out var geometry))
    {
      throw new Exception("La respuesta de ORS no contiene la geometría de la ruta.");
    }
    var instructions = new List<RouteInstructionDto>();
    if (route.TryGetProperty("segments", out var segments))
    {
      foreach (var segment in segments.EnumerateArray())
      {
        if (segment.TryGetProperty("steps", out var steps))
        {
          foreach (var step in steps.EnumerateArray())
          {
            var instruction = step.TryGetProperty("instruction", out var instr)
                ? instr.GetString() : "Paso sin descripción";
            var stepDistance = step.TryGetProperty("distance", out var dist)
                ? dist.GetDouble() : 0.0;
            var travelMode = profile.Split('-')[0];

            instructions.Add(new RouteInstructionDto
            {
              Instruction = instruction,
              Distance = stepDistance,
              Mode = travelMode
            });
          }
        }
      }
    }
    var polyline = geometry.GetString();
    var coords = DecodeGooglePolyline(polyline);

    return new RouteResponseDto
    {
      RouteId = Guid.NewGuid().ToString(),
      Distance = summary.GetProperty("distance").GetDouble(),
      Duration = summary.GetProperty("duration").GetDouble(),
      Geometry = coords,
      Instructions = instructions
    };
  }

  private async Task<RouteResponseDto> CalcularConGoogleMapsAsync(RouteRequestDto request)
  {
    var apiKey = this._config["APIKeys:GoogleMaps:ApiKey"];
    string origin = $"{request.OriginLat.ToString(System.Globalization.CultureInfo.InvariantCulture)},{request.OriginLng.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
    string destination = $"{request.DestinationLat.ToString(System.Globalization.CultureInfo.InvariantCulture)},{request.DestinationLng.ToString(System.Globalization.CultureInfo.InvariantCulture)}";
    var queryParams = new List<string>
        {
            $"origin={Uri.EscapeDataString(origin)}",
            $"destination={Uri.EscapeDataString(destination)}",
            "mode=transit",
            "departure_time=now",
            "transit_routing_preference=fewer_transfers",
            "alternatives=true",
        };

    if (request.Preference == "rail")
    {
      queryParams.Add("transit_mode=rail");
    }

    else if (request.Preference == "bus")
    {
      queryParams.Add("transit_mode=bus");
    }

    var url = $"https://maps.googleapis.com/maps/api/directions/json?{string.Join("&", queryParams)}&key={apiKey}";
    var res = await this._httpClient.GetAsync(new Uri(url)).ConfigureAwait(false);
    if (!res.IsSuccessStatusCode)
    {
      throw new Exception($"Error en la solicitud a Google Maps: {res.StatusCode}");
    }

    var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);

    using var doc = JsonDocument.Parse(json);
    if (!doc.RootElement.TryGetProperty("status", out var status) || status.GetString() != "OK")
    {
      throw new Exception($"Error en la respuesta de Google Maps: {doc.RootElement.GetProperty("status").GetString()}");
    }

    if (!doc.RootElement.TryGetProperty("routes", out var routes) || routes.GetArrayLength() == 0)
    {
      throw new Exception("No se encontraron rutas de transporte público para los puntos proporcionados.");
    }


    var route = routes[0];
    if (!route.TryGetProperty("legs", out var legs) || legs.GetArrayLength() == 0)
    {
      throw new Exception("La ruta no contiene tramos válidos.");
    }

    var leg = legs[0];
    if (!leg.TryGetProperty("distance", out var distance) || !leg.TryGetProperty("duration", out var duration))
    {
      throw new Exception("La ruta no contiene información de distancia o duración.");
    }

    if (!route.TryGetProperty("overview_polyline", out var polylineElement) ||
        !polylineElement.TryGetProperty("points", out var points))
    {
      throw new Exception("La ruta no contiene una polilínea válida.");
    }
    var polyline = points.GetString();
    if (string.IsNullOrEmpty(polyline))
    {
      throw new Exception("La polilínea de la ruta está vacía.");
    }
    var instructions = new List<RouteInstructionDto>();
    if (leg.TryGetProperty("steps", out var steps))
    {
      foreach (var step in steps.EnumerateArray())
      {
        var instruction = step.TryGetProperty("html_instructions", out var htmlInstructions)
            ? htmlInstructions.GetString() : "Paso sin descripción";
        var stepDistance = step.TryGetProperty("distance", out var dist)
            ? dist.GetProperty("value").GetDouble() : 0.0;
        var travelMode = step.TryGetProperty("travel_mode", out var mode)
            ? mode.GetString().ToUpperInvariant() : "unknown";

        instructions.Add(new RouteInstructionDto
        {
          Instruction = System.Net.WebUtility.HtmlDecode(instruction),
          Distance = stepDistance,
          Mode = travelMode
        });
      }

    }
    var coords = DecodeGooglePolyline(polyline);

    return new RouteResponseDto
    {
      RouteId = Guid.NewGuid().ToString(),
      Distance = distance.GetProperty("value").GetDouble(),
      Duration = duration.GetProperty("value").GetDouble(),
      Geometry = coords,
      Instructions = instructions
    };
  }

  private static List<double[]> DecodeGooglePolyline(string polyline)
  {
    var coords = new List<double[]>();
    int index = 0, len = polyline.Length;
    int lat = 0, lng = 0;

    while (index < len)
    {
      int b, shift = 0, result = 0;
      do
      {
        b = polyline[index++] - 63;
        result |= (b & 0x1f) << shift;
        shift += 5;
      } while (b >= 0x20);
      int dlat = ((result & 1) != 0) ? ~(result >> 1) : result >> 1;
      lat += dlat;

      shift = 0;
      result = 0;
      do
      {
        b = polyline[index++] - 63;
        result |= (b & 0x1f) << shift;
        shift += 5;
      } while (b >= 0x20);
      int dlng = ((result & 1) != 0) ? ~(result >> 1) : result >> 1;
      lng += dlng;

      coords.Add([lng / 1E5, lat / 1E5]);
    }

    return coords;
  }
  public async Task<string> GetStreetNameAsync(double lat, double lon)
  {
    var apiKey = this._config["APIKeys:GoogleMaps:ApiKey"];
    var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat.ToString(CultureInfo.InvariantCulture)},{lon.ToString(CultureInfo.InvariantCulture)}&key={apiKey}";

    var res = await this._httpClient.GetAsync(new Uri(url)).ConfigureAwait(false);
    if (!res.IsSuccessStatusCode) return "Unknown";

    var json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
    using var doc = JsonDocument.Parse(json);

    var results = doc.RootElement.GetProperty("results");
    if (results.GetArrayLength() == 0) return "Unknown";

    var addressComponents = results[0].GetProperty("address_components");
    foreach (var component in addressComponents.EnumerateArray())
    {
      if (component.GetProperty("types").EnumerateArray().Any(t => t.GetString() == "route"))
      {
        return component.GetProperty("long_name").GetString();
      }
    }

    return results[0].GetProperty("formatted_address").GetString();
  }
  public async Task<bool> SaveRoute(RouteDto route)
  {
    if (route == null)
    {
      throw new ArgumentNullException(nameof(route), "El parámetro route no puede ser null.");
    }
    var user = await this._userRepository.GetUserById(route.UserId).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }

    var routeEntity = new RouteEntity
    {
      RouteId = Guid.Parse(route.RouteId),
      OriginLat = route.OriginLat,
      OriginLng = route.OriginLng,
      DestinationLat = route.DestinationLat,
      DestinationLng = route.DestionationLng,
      Mean = route.mean,
      Preference = route.Preference,
      Distance = (float)route.Distance,
      Duration = (float)route.Duration,
      GeometryJson = JsonSerializer.Serialize(route.Geometry),
      InstructionsJson = JsonSerializer.Serialize(route.Instructions),
      OriginStreetName = route.OriginStreetName,
      DestinationStreetName = route.DestinationStreetName,
      UserId = Guid.Parse(user.UserId)
    };
    return await this._routeRepository.GuardarRutaAsync(routeEntity).ConfigureAwait(false);
  }
  public async Task<bool> DeleteRoute(string routeId)
  {
    return await this._routeRepository.DeleteRoute(routeId).ConfigureAwait(false);
  }
  public async Task<bool> PublishRoute(PublishedRouteDto publishedRouteDto)
  {
    return await this._routeRepository.PublishRoute(publishedRouteDto).ConfigureAwait(false);
  }
  public async Task<bool> DeletePublishedRoute(string routeId)
  {
    return await this._routeRepository.DeletePublishedRoute(routeId).ConfigureAwait(false);
  }
  public async Task<List<PublishedRouteDto>> GetRoutesNearAsync(double lat, double lon, double radiusInMeters)
  {
    return await this._routeRepository.GetRoutesNearAsync(lat, lon, radiusInMeters).ConfigureAwait(false);
  }
  public async Task<List<RouteDto>> GetSavedRoute(string userId)
  {
    var user = await this._userRepository.GetUserById(userId).ConfigureAwait(false) ?? throw new Exception("Usuario no encontrado");
    return await this._routeRepository.GetSavedRoute(user.UserId).ConfigureAwait(false);
  }
}

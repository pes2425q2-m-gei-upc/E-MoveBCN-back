using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;

public class RouteService : IRouteService
{
    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;
    private readonly IRouteRepository _repo;

    public RouteService(IConfiguration config, HttpClient httpClient, IRouteRepository repo)
    {
        _config = config;
        _httpClient = httpClient;
    }

    public async Task<RouteResponseDto> CalcularRutaAsync(RouteRequestDto request, Guid usuarioId)
    {
        var response = request.Mode == "transit"
            ? await CalcularConGoogleMapsAsync(request)
            : await CalcularConORSAsync(request);

        var route = new RouteEntity
        {
            Id = Guid.NewGuid(),
            OriginLat = request.OriginLat,
            OriginLng = request.OriginLng,
            DestinationLat = request.DestinationLat,
            DestinationLng = request.DestinationLng,
            Mean = request.Mode,
            Preference = request.Preference,
            Distance = (float)response.Distance,
            Duration = (float)response.Duration,
            GeometryJson = JsonSerializer.Serialize(response.Geometry)
        };

        return response;
    }

    public async Task<RouteResponseDto> CalcularConORSAsync(RouteRequestDto request)
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
            "wheelchair" => "wheelchair",
            "sustainable" => "cycling-regular", 
            _ => "cycling-regular"
        };

        var apiKey = _config["APIKeys:ORS:ApiKey"];
        var url = $"https://api.openrouteservice.org/v2/directions/{profile}";
        _httpClient.DefaultRequestHeaders.Add("Authorization", apiKey);

        var body = new
        {
            coordinates = new[]
            {
                new[] { request.OriginLng, request.OriginLat },
                new[] { request.DestinationLng, request.DestinationLat }
            },
            preference = request.Preference switch
            {
                "shortest" => "shortest",
                _ => request.Mode == "sustainable" ? "shortest" : "recommended"
            }
            
        };

        var content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
        var res = await _httpClient.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
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
        var polyline = geometry.GetString();
        var coords = DecodeGooglePolyline(polyline);

        return new RouteResponseDto
        {
            Distance = summary.GetProperty("distance").GetDouble(),
            Duration = summary.GetProperty("duration").GetDouble(),
            Geometry = coords
        };
    }

    public async Task<RouteResponseDto> CalcularConGoogleMapsAsync(RouteRequestDto request)
    {
        var apiKey = _config["APIKeys:GoogleMaps:ApiKey"];
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
        var res = await _httpClient.GetAsync(url);
        if (!res.IsSuccessStatusCode)
        {
            throw new Exception($"Error en la solicitud a Google Maps: {res.StatusCode}");
        }

        var json = await res.Content.ReadAsStringAsync();

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
        var coords = DecodeGooglePolyline(polyline);

        return new RouteResponseDto
        {
            Distance = distance.GetProperty("value").GetDouble(), 
            Duration = duration.GetProperty("value").GetDouble(), 
            Geometry = coords
        };
    }

    private List<double[]> DecodeGooglePolyline(string polyline)
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

            coords.Add(new double[] { lng / 1E5, lat / 1E5 });
        }

        return coords;
    }
}

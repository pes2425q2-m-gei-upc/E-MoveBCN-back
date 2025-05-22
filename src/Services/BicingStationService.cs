using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Bicing;
using Entity.Bicing;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
using Services.Interface;
namespace Services;
public class BicingStationService(HttpClient httpClient, ILogger<BicingStationService> logger,
                          IBicingStationRepository bicingStationRepository) : IBicingStationService
{
  private readonly IBicingStationRepository _bicingStationRepository = bicingStationRepository;
  private readonly HttpClient _httpClient = httpClient;
  private readonly ILogger<BicingStationService> _logger = logger;
  private readonly string _apiToken = "2185586da4efca30fd5d2d22aec924e5c7e7459cafa210c2df828d3743eef3f4";

  private JsonElement? _cachedJsonData;
  private DateTime _lastFetchTime = DateTime.MinValue;
  private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

  public async Task<List<BicingStationDto>> GetAllBicingStations()
  {
    return await this._bicingStationRepository.GetAllBicingStations().ConfigureAwait(false);
  }

  public async Task FetchAndStoreBicingStationsAsync()
  {
    try
    {
      await RefreshCacheIfNeededAsync().ConfigureAwait(false);
      var bicingStationsToAdd = ProcessStationsDataAsync();
      await this._bicingStationRepository.BulkInsertAsync(bicingStationsToAdd).ConfigureAwait(false);

      this._logger.LogInformation("Datos insertados: {Stations} estaciones", bicingStationsToAdd.Count);
    }
    catch (Exception ex)
    {
      this._logger.LogError(ex, "Error general al obtener y almacenar estaciones de bici");
      throw;
    }
  }

  private async Task RefreshCacheIfNeededAsync()
  {
    if (this._cachedJsonData == null || DateTime.Now - this._lastFetchTime > this._cacheDuration)
    {

      var requestUrl = "https://opendata-ajuntament.barcelona.cat/data/ca/dataset/informacio-estacions-bicing/resource/f60e9291-5aaa-417d-9b91-612a9de800aa/download/recurs.json";
      this._httpClient.DefaultRequestHeaders.Clear();
      this._httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this._apiToken);

      var response = await this._httpClient.GetAsync(new Uri(requestUrl)).ConfigureAwait(false);
      response.EnsureSuccessStatusCode();

      var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      this._cachedJsonData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
      this._lastFetchTime = DateTime.Now;
    }
  }

  private List<BicingStationEntity> ProcessStationsDataAsync()
  {
    if (!this._cachedJsonData.HasValue || !this._cachedJsonData.Value.TryGetProperty("data", out var dataElement))
    {
      this._logger.LogError("El JSON no tiene la estructura esperada 'data'");
      return [];
    }

    var bicingStationsToAdd = new List<BicingStationEntity>();

    if (dataElement.TryGetProperty("stations", out var stationsElement))
    {
      foreach (var stationElement in stationsElement.EnumerateArray())
      {
        var station = ProcessSingleStation(stationElement);
        bicingStationsToAdd.Add(station);
      }
    }

    return bicingStationsToAdd;
  }

  private BicingStationEntity ProcessSingleStation(JsonElement stationElement)
  {
    if (!stationElement.TryGetProperty("station_id", out var idElement))
    {
      this._logger.LogError("Estación sin station_id, omitiendo");
      return null;
    }

    var stationId = idElement.GetInt32();
    this._logger.LogDebug("Procesando estación ID: {StationId}", stationId);

    try
    {

      string name = null;
      if (stationElement.TryGetProperty("name", out var nameElement))
      {
        name = nameElement.ValueKind == JsonValueKind.Number
            ? nameElement.GetInt32().ToString(System.Globalization.CultureInfo.InvariantCulture)
            : nameElement.GetString();
      }
      else
      {
        throw new KeyNotFoundException("Falta propiedad requerida: name");
      }


      string postCode = "00000";
      if (stationElement.TryGetProperty("post_code", out var postCodeElement))
      {
        postCode = postCodeElement.ValueKind == JsonValueKind.Number
            ? postCodeElement.GetInt32().ToString("D5", System.Globalization.CultureInfo.InvariantCulture)
            : postCodeElement.GetString() ?? "00000";
      }


      string address = null;
      if (stationElement.TryGetProperty("address", out var addressElement))
      {
        address = addressElement.ValueKind == JsonValueKind.Number
            ? nameElement.GetInt32().ToString(System.Globalization.CultureInfo.InvariantCulture)
            : addressElement.GetString();
      }
      else
      {
        throw new KeyNotFoundException("Falta propiedad requerida: address");
      }


      string crossStreet = null;
      if (stationElement.TryGetProperty("cross_street", out var crossStreetElement) &&
          crossStreetElement.ValueKind != JsonValueKind.Null)
      {
        crossStreet = crossStreetElement.ValueKind == JsonValueKind.Number
            ? nameElement.GetInt32().ToString(System.Globalization.CultureInfo.InvariantCulture)
            : crossStreetElement.GetString();
      }

      return new BicingStationEntity
      {
        BicingId = stationId,
        BicingName = name,
        Latitude = stationElement.TryGetProperty("lat", out var latElement) && latElement.ValueKind != JsonValueKind.Null
              ? latElement.GetDouble()
              : 0.0,
        Longitude = stationElement.TryGetProperty("lon", out var lonElement) && lonElement.ValueKind != JsonValueKind.Null
              ? lonElement.GetDouble()
              : 0.0,
        Altitude = stationElement.TryGetProperty("altitude", out var altElement) && altElement.ValueKind != JsonValueKind.Null
              ? altElement.GetDouble()
              : 0.0,
        Address = address,
        CrossStreet = crossStreet,
        PostCode = postCode,
        Capacity = stationElement.TryGetProperty("capacity", out var capacityElement) && capacityElement.ValueKind != JsonValueKind.Null
              ? capacityElement.GetInt32()
              : 0,
        IsChargingStation = stationElement.TryGetProperty("is_charging_station", out var chargingElement) && chargingElement.ValueKind != JsonValueKind.Null
&& chargingElement.GetBoolean()
      };
    }
    catch (KeyNotFoundException ex)
    {
      this._logger.LogError(ex, "Error procesando propiedades de la estación {StationId}", stationId);
      return null;
    }
    catch (JsonException ex)
    {
      this._logger.LogError(ex, "Error de deserialización JSON en la estación {StationId}", stationId);
      return null;
    }
    catch (Exception ex)
    {
      this._logger.LogError(ex, "Error inesperado procesando la estación {StationId}", stationId);
      throw;
    }
  }
}

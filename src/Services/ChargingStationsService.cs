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

public class ChargingStationsService(HttpClient httpClient, ILogger<ChargingStationsService> logger, IChargingStationsRepository chargingStationsRepository) : IChargingStationsService
{
  private readonly IChargingStationsRepository _chargingStationsRepository = chargingStationsRepository;
  private readonly HttpClient _httpClient = httpClient;
  private readonly ILogger<ChargingStationsService> _logger = logger;
  private readonly string _apiToken = "2185586da4efca30fd5d2d22aec924e5c7e7459cafa210c2df828d3743eef3f4";

  private JsonElement? _cachedJsonData;
  private DateTime _lastFetchTime = DateTime.MinValue;
  private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

  public async Task<List<ChargingStationDto>> GetAllChargingStationsAsync()
  {
    return await this._chargingStationsRepository.GetAllChargingStations().ConfigureAwait(false);
  }

  public async Task FetchAndStoreChargingStationsAsync()
  {
    try
    {
      if (this._cachedJsonData == null || DateTime.Now - this._lastFetchTime > this._cacheDuration)
      {
        var requestUrl = "https://opendata-ajuntament.barcelona.cat/data/dataset/8cdafa08-d378-4bf1-aad4-fafffe815940/resource/9febc26f-d6a7-45f2-8f73-f529ba4da930/download";
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this._apiToken);

        var response = await this._httpClient.GetAsync(new Uri(requestUrl)).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          this._logger.LogError("HTTP {StatusCode} al obtener estaciones de carga: {ReasonPhrase}", 
            response.StatusCode, response.ReasonPhrase);
          return;
        }

        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        this._cachedJsonData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        this._lastFetchTime = DateTime.Now;
      }

      var locationsToAdd = new List<LocationEntity>();
      var hostsToAdd = new List<HostEntity>();
      var stationsToAdd = new List<StationEntity>();
      var portsToAdd = new List<PortEntity>();

      if (!this._cachedJsonData.HasValue || !this._cachedJsonData.Value.TryGetProperty("locations", out var locationsElement))
      {
        this._logger.LogError("El JSON recibido no contiene la clave 'locations'. JSON: {JsonResponse}", this._cachedJsonData);
        return;
      }

      foreach (var locationElement in locationsElement.EnumerateArray())
      {
        var locationId = locationElement.GetProperty("id").GetString();
        locationsToAdd.Add(new LocationEntity
        {
          LocationId = locationId,
          NetworkBrandName = locationElement.GetProperty("network_brand_name").GetString(),
          OperatorPhone = locationElement.GetProperty("contact").GetProperty("operator_phone").GetString(),
          OperatorWebsite = locationElement.GetProperty("contact").GetProperty("operator_website").GetString(),
          Latitude = locationElement.GetProperty("coordinates").GetProperty("latitude").GetDouble(),
          Longitude = locationElement.GetProperty("coordinates").GetProperty("longitude").GetDouble(),
          AddressString = locationElement.GetProperty("address").GetProperty("address_string").GetString(),
          Locality = locationElement.GetProperty("address").GetProperty("locality").GetString(),
          PostalCode = locationElement.GetProperty("address").GetProperty("postal_code").GetString()
        });

        if (locationElement.TryGetProperty("host", out var hostElement))
        {
          hostsToAdd.Add(new HostEntity
          {
            HostId = Guid.NewGuid(),
            HostName = hostElement.GetProperty("name").GetString(),
            HostAddress = hostElement.GetProperty("address").GetProperty("address_string").GetString(),
            HostLocality = hostElement.GetProperty("address").GetProperty("locality").GetString(),
            HostPostalCode = hostElement.GetProperty("address").GetProperty("postal_code").GetString(),
            OperatorPhone = hostElement.GetProperty("contact").GetProperty("operator_phone").GetString(),
            OperatorWebsite = hostElement.GetProperty("contact").GetProperty("operator_website").GetString(),
            LocationId = locationId
          });
        }

        if (locationElement.TryGetProperty("stations", out var stationsElement))
        {
          foreach (var stationElement in stationsElement.EnumerateArray())
          {
            var stationId = stationElement.GetProperty("id").GetString();

            string stationLabel = null;
            if (stationElement.TryGetProperty("label", out var labelElement))
            {
              stationLabel = labelElement.GetString();
            }
            else
            {
              this._logger.LogWarning("Propiedad 'label' no encontrada para la estación {StationId}. Usando valor por defecto.", stationId);
            }

            float stationLatitude = 0;
            float stationLongitude = 0;

            if (stationElement.TryGetProperty("coordinates", out var coordinatesElement))
            {
              if (coordinatesElement.TryGetProperty("latitude", out var latElement))
              {
                stationLatitude = (float)latElement.GetDouble();
              }
              if (coordinatesElement.TryGetProperty("longitude", out var lonElement))
              {
                stationLongitude = (float)lonElement.GetDouble();
              }
            }
            else
            {
              this._logger.LogWarning("Propiedad 'coordinates' no encontrada para la estación {StationId}.", stationId);
            }

            stationsToAdd.Add(new StationEntity
            {
              StationId = stationId,
              StationLabel = stationLabel,
              StationLatitude = stationLatitude,
              StationLongitude = stationLongitude,
              LocationId = locationId
            });

            if (stationElement.TryGetProperty("ports", out var portsElement))
            {
              foreach (var portElement in portsElement.EnumerateArray())
              {
                string portId = portElement.TryGetProperty("id", out var idElement) ?
                    idElement.GetString() : "port-" + Guid.NewGuid().ToString();

                string connectorType = portElement.TryGetProperty("connector_type", out var connectorElement) ?
                    connectorElement.GetString() : "Unknown";

                double powerKw = portElement.TryGetProperty("power_kw", out var powerElement) ?
                    powerElement.GetDouble() : 0;

                string chargingMechanism = portElement.TryGetProperty("charging_mechanism", out var mechanismElement) ?
                    mechanismElement.GetString() : "Unknown";

                string status = "Unknown";
                if (portElement.TryGetProperty("port_status", out var statusArray) &&
                    statusArray.GetArrayLength() > 0 &&
                    statusArray[0].TryGetProperty("status", out var statusElement))
                {
                  status = statusElement.GetString();
                }

                bool reservable = portElement.TryGetProperty("reservable", out var reservableElement) &&
                    reservableElement.GetBoolean();

                DateTime lastUpdated = DateTime.Now;
                if (portElement.TryGetProperty("last_updated", out var lastUpdatedElement))
                {
                  try
                  {
                    lastUpdated = DateTime.Parse(lastUpdatedElement.GetString(), System.Globalization.CultureInfo.InvariantCulture);
                  }
                  catch (FormatException)
                  {
                    this._logger.LogWarning("Formato de fecha inválido para last_updated en puerto {PortId}", portId);
                  }
                }

                portsToAdd.Add(new PortEntity
                {
                  PortId = portId,
                  ConnectorType = connectorType,
                  PowerKw = powerKw,
                  ChargingMechanism = chargingMechanism,
                  Status = status,
                  Reservable = reservable,
                  LastUpdated = lastUpdated,
                  StationId = stationId
                });
              }
            }
          }
        }
      }

      await this._chargingStationsRepository.BulkInsertAsync(locationsToAdd, hostsToAdd, stationsToAdd, portsToAdd).ConfigureAwait(false);
      this._logger.LogInformation("Datos insertados: {Locations} ubicaciones, {Hosts} hosts, {Stations} estaciones, {Ports} puertos",
          locationsToAdd.Count, hostsToAdd.Count, stationsToAdd.Count, portsToAdd.Count);
    }
    catch (HttpRequestException ex)
    {
      this._logger.LogError(ex, "Fallo de red al obtener estaciones de carga");
    }
    catch (JsonException ex)
    {
      this._logger.LogError(ex, "Error al deserializar el JSON de estaciones de carga");
    }
    catch (Exception ex)
    {
      this._logger.LogError(ex, "Error inesperado al obtener y almacenar estaciones de carga");
    }
  }
}

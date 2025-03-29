using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Entity;
using System.Linq;
using Repositories.Interface;
using Dto;
using Services.Interface;

namespace Services
{
    public class BicingStationService : IBicingStationService
    {
        private readonly IBicingStationRepository _bicingStationRepository;
        private readonly HttpClient _httpClient;
        private readonly ILogger<BicingStationService> _logger;
        private readonly string _apiToken = "2185586da4efca30fd5d2d22aec924e5c7e7459cafa210c2df828d3743eef3f4";

        private JsonElement? _cachedJsonData;
        private DateTime _lastFetchTime = DateTime.MinValue;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

        public BicingStationService(HttpClient httpClient, ILogger<BicingStationService> logger, 
                                  IBicingStationRepository bicingStationRepository)
        {
            _httpClient = httpClient;
            _logger = logger;
            _bicingStationRepository = bicingStationRepository;
        }

        public async Task<List<BicingStationDto>> GetAllBicingStationsAsync()
        {
            return await _bicingStationRepository.GetAllBicingStations();
        }

        public async Task FetchAndStoreBicingStationsAsync()
        {
            try
            {
                await RefreshCacheIfNeededAsync();
                var bicingStationsToAdd = await ProcessStationsDataAsync();
                await _bicingStationRepository.BulkInsertAsync(bicingStationsToAdd);
                
                _logger.LogInformation("Datos insertados: {Stations} estaciones", bicingStationsToAdd.Count);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error general al obtener y almacenar estaciones de bici");
                throw;
            }
        }

        private async Task RefreshCacheIfNeededAsync()
        {
            if (_cachedJsonData == null || DateTime.Now - _lastFetchTime > _cacheDuration)
            {
                
                var requestUrl = "https://opendata-ajuntament.barcelona.cat/data/ca/dataset/informacio-estacions-bicing/resource/f60e9291-5aaa-417d-9b91-612a9de800aa/download/recurs.json";
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_apiToken);

                var response = await _httpClient.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();

                var jsonResponse = await response.Content.ReadAsStringAsync();
                _cachedJsonData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
                _lastFetchTime = DateTime.Now;
            }
        }

        private async Task<List<BicingStationEntity>> ProcessStationsDataAsync()
        {
            if (!_cachedJsonData.HasValue || !_cachedJsonData.Value.TryGetProperty("data", out var dataElement))
            {
                _logger.LogError("El JSON no tiene la estructura esperada 'data'");
                return new List<BicingStationEntity>();
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
                _logger.LogError("Estación sin station_id, omitiendo");
                return null;
            }

            var stationId = idElement.GetInt32();
            _logger.LogDebug("Procesando estación ID: {StationId}", stationId);

            try
            {
                
                string name = null;
                if (stationElement.TryGetProperty("name", out var nameElement))
                {
                    name = nameElement.ValueKind == JsonValueKind.Number 
                        ? nameElement.GetInt32().ToString() 
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
                        ? postCodeElement.GetInt32().ToString("D5") 
                        : postCodeElement.GetString() ?? "00000";
                }

                
                string address = null;
                if (stationElement.TryGetProperty("address", out var addressElement))
                {
                    address = addressElement.ValueKind == JsonValueKind.Number 
                        ? addressElement.GetInt32().ToString() 
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
                        ? crossStreetElement.GetInt32().ToString() 
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
                        ? chargingElement.GetBoolean()
                        : false
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error procesando propiedades de la estación {StationId}", stationId);
                return null;
            }
        }
    }
}
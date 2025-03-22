using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Entity;

using Repositories.Interface;
using Dto;
using Repositories;
using Services.Interface;
namespace Services;

public class DadesObertesService : IDadesObertesService
{
    private readonly IDadesObertesRepository _DadesObertesRepository;
    private readonly HttpClient _httpClient;
    private readonly ILogger<DadesObertesService> _logger;
    private readonly string _apiToken = "2185586da4efca30fd5d2d22aec924e5c7e7459cafa210c2df828d3743eef3f4";

    public DadesObertesService(HttpClient httpClient, ILogger<DadesObertesService> logger, IDadesObertesRepository dadesObertesRepository)
    {
        _httpClient = httpClient;
        _logger = logger;
        _DadesObertesRepository = dadesObertesRepository;
    }

    public async Task<List<StationDto>> GetAllStationsAsync()
    {
            return await _DadesObertesRepository.GetAllStations();
    }

    public async Task FetchAndStoreChargingStationsAsync()
    {
        var requestUrl = "https://opendata-ajuntament.barcelona.cat/data/dataset/8cdafa08-d378-4bf1-aad4-fafffe815940/resource/9febc26f-d6a7-45f2-8f73-f529ba4da930/download";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

        var response = await _httpClient.GetAsync(requestUrl);

        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var jsonData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);

            var locationsElement = jsonData.GetProperty("locations");

            foreach (var locationElement in locationsElement.EnumerateArray())
            {
                var Location = new LocationDto
                {
                    LocationId = locationElement.GetProperty("id").GetString(),
                    NetworkBrandName = locationElement.GetProperty("network_brand_name").GetString(),
                    OperatorPhone = locationElement.GetProperty("contact").GetProperty("operator_phone").GetString(),
                    OperatorWebsite = locationElement.GetProperty("contact").GetProperty("operator_website").GetString(),
                    Latitude = locationElement.GetProperty("coordinates").GetProperty("latitude").GetDouble(),
                    Longitude = locationElement.GetProperty("coordinates").GetProperty("longitude").GetDouble(),
                    AddressString = locationElement.GetProperty("address").GetProperty("address_string").GetString(),
                    Locality = locationElement.GetProperty("address").GetProperty("locality").GetString(),
                    PostalCode = locationElement.GetProperty("address").GetProperty("postal_code").GetString()
                };

                await _DadesObertesRepository.AddLocationAsync(Location);
                
                if (locationElement.TryGetProperty("host", out var hostElement))
                {
                    var Host = new HostDto
                    {
                        HostId = Guid.NewGuid().ToString(),
                        HostName = hostElement.GetProperty("name").GetString(),
                        HostAddress = hostElement.GetProperty("address").GetProperty("address_string").GetString(),
                        HostLocality = hostElement.GetProperty("address").GetProperty("locality").GetString(),
                        HostPostalCode = hostElement.GetProperty("address").GetProperty("postal_code").GetString(),
                        OperatorPhone = hostElement.GetProperty("contact").GetProperty("operator_phone").GetString(),
                        OperatorWebsite = hostElement.GetProperty("contact").GetProperty("operator_website").GetString(),
                        LocationId = Location.LocationId
                    };
                   await _DadesObertesRepository.AddHostAsync(Host);
                }

                if (locationElement.TryGetProperty("stations", out var stationsElement))
                {
                    foreach (var stationElement in stationsElement.EnumerateArray())
                    {
                        var Station = new StationDto
                        {
                            StationId = stationElement.GetProperty("id").GetString(),
                            StationLabel = stationElement.GetProperty("label").GetString(),
                            StationLatitude = stationElement.GetProperty("coordinates").GetProperty("latitude").GetDouble(),
                            StationLongitude = stationElement.GetProperty("coordinates").GetProperty("longitude").GetDouble(),
                            Reservable = stationElement.GetProperty("ports")[0].GetProperty("reservable").GetBoolean(), 
                            LocationId = Location.LocationId
                        };
                        await _DadesObertesRepository.AddStationAsync(Station);

                        
                        if (stationElement.TryGetProperty("ports", out var portsElement))
                        {
                            foreach (var portElement in portsElement.EnumerateArray())
                            {
                                var Port = new PortDto
                                {
                                    PortId = portElement.GetProperty("id").GetString(),
                                    ConnectorType = portElement.GetProperty("connector_type").GetString(),
                                    PowerKw = portElement.GetProperty("power_kw").GetDouble(),
                                    ChargingMechanism = portElement.GetProperty("charging_mechanism").GetString(),
                                    PortStatus = portElement.GetProperty("port_status")[0].GetProperty("status").GetString(), 
                                    LastUpdated = DateTime.Parse(portElement.GetProperty("last_updated").GetString()),
                                    StationId = Station.StationId
                                };
                                await _DadesObertesRepository.AddPortAsync(Port);
                            }
                        }
                    }
                    
                }
            }
        }
        else
        {
            _logger.LogError($"Error fetching charging stations: {response.StatusCode}");
        }
    }
}

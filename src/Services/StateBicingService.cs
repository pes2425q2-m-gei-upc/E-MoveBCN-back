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
public class StateBicingService(HttpClient httpClient, ILogger<StateBicingService> logger, IStateBicingRepository dadesObertesRepository) : IStateBicingService
{
  private readonly IStateBicingRepository _stateBicingRepository = dadesObertesRepository;
  private readonly HttpClient _httpClient = httpClient;
  private readonly ILogger<StateBicingService> _logger = logger;
  private readonly string _apiToken = "2185586da4efca30fd5d2d22aec924e5c7e7459cafa210c2df828d3743eef3f4";

  private JsonElement? _cachedJsonData;
  private DateTime _lastFetchTime = DateTime.MinValue;
  private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(30);

  public async Task<List<StateBicingDto>> GetAllStateBicingStationsAsync()
  {
    return await this._stateBicingRepository.GetAllStateBicingStations().ConfigureAwait(false);
  }

  public async Task FetchAndStoreStateBicingStationsAsync()
  {
    try
    {
      if (this._cachedJsonData == null || DateTime.Now - this._lastFetchTime > this._cacheDuration)
      {
        var requestUrl = "https://opendata-ajuntament.barcelona.cat/data/ca/dataset/estat-estacions-bicing/resource/1b215493-9e63-4a12-8980-2d7e0fa19f85/download/recurs.json";
        this._httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(this._apiToken);

        var response = await this._httpClient.GetAsync(new Uri(requestUrl)).ConfigureAwait(false);
        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        this._cachedJsonData = JsonSerializer.Deserialize<JsonElement>(jsonResponse);
        this._lastFetchTime = DateTime.Now;
      }

      if (!this._cachedJsonData.HasValue || !this._cachedJsonData.Value.TryGetProperty("data", out var dataElement))
      {
        this._logger.LogError("El JSON no tiene la estructura esperada 'data'");
        return;
      }

      var stateBicingToAdd = new List<StateBicingEntity>();

      if (dataElement.TryGetProperty("stations", out var statesElement))
      {
        foreach (var stateElement in statesElement.EnumerateArray())
        {
          var Bicingid = stateElement.GetProperty("station_id").GetInt32();

          stateBicingToAdd.Add(new StateBicingEntity
          {
            BicingId = Bicingid,
            NumBikesAvailable = stateElement.GetProperty("num_bikes_available").GetInt32(),
            NumBikesAvailableMechanical = stateElement.GetProperty("num_bikes_available_types").GetProperty("mechanical").GetInt32(),
            NumBikesAvailableEbike = stateElement.GetProperty("num_bikes_available_types").GetProperty("ebike").GetInt32(),
            NumDocksAvailable = stateElement.GetProperty("num_docks_available").GetInt32(),
            LastReported = DateTimeOffset.FromUnixTimeSeconds(stateElement.GetProperty("last_reported").GetInt64()).DateTime,
            Status = stateElement.GetProperty("status").GetString()
          });
        }
      }
      await this._stateBicingRepository.BulkInsertAsync(stateBicingToAdd).ConfigureAwait(false);
      this._logger.LogInformation("Datos insertados: {State} estados",
          stateBicingToAdd.Count);
    }
    catch (Exception ex)
    {
      this._logger.LogError(ex, "Error general al obtener y almacenar el estado de las estaciones de bicing");
      throw;
    }
  }
}

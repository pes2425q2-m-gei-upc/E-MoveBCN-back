using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Dto.Air;
using Helpers.Interface;

namespace Helpers;

public class AireLliureHelper(HttpClient httpClient) : IAireLliureHelper
{
  private readonly HttpClient _httpClient = httpClient;

  public async Task<List<AirSensorDto>> GetAllAirSensorsAsync()
  {
    var url = "https://airelliure-backend.onrender.com/estacions-qualitat-aire";
    try
    {
      var uri = new Uri(url);
      var response = await this._httpClient.GetAsync(uri).ConfigureAwait(false);
      response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

      var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
      var options = new JsonSerializerOptions
      {
        PropertyNameCaseInsensitive = false
      };

      var sensors = JsonSerializer.Deserialize<List<AirSensorDto>>(json, options);
      return sensors ?? [];
    }
    catch (HttpRequestException ex)
    {
      // Log the exception if needed
      Console.WriteLine($"HTTP error retrieving sensor data: {ex.Message}");
      return [];
    }
    catch (JsonException ex)
    {
      // Log the exception if needed
      Console.WriteLine($"JSON error retrieving sensor data: {ex.Message}");
      return [];
    }
  }
  // ✅ Find closest sensor
  public double? FindClosestSensor(List<AirSensorDto> sensors, double targetLat, double targetLon)
  {
    var result = sensors
        .OrderBy(s => HaversineDistance(targetLat, targetLon, s.Latitud, s.Longitud))
        .FirstOrDefault();
    return result?.IndexQualitatAire;
  }

  // ✅ Haversine formula to calculate great-circle distance between two points
  private static double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
  {
    const double R = 6371; // Radius of Earth in kilometers
    var dLat = DegreesToRadians(lat2 - lat1);
    var dLon = DegreesToRadians(lon2 - lon1);

    var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
            Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
            Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

    var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
    return R * c;
  }

  private static double DegreesToRadians(double deg)
  {
    return deg * (Math.PI / 180);
  }
}

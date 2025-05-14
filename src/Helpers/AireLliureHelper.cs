using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using src.Dto.Air;
using src.Helpers.Interface;

namespace src.Helpers;

public class AireLliureHelper : IAireLliureHelper
{
    private readonly HttpClient _httpClient;
    public AireLliureHelper(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
  public async Task<List<AirSensorDto>> GetAllAirSensorsAsync()
  {
    var url = "https://airelliure-backend.onrender.com/estacions-qualitat-aire";
        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws if status code is not 2xx

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = false
            };

            var sensors = JsonSerializer.Deserialize<List<AirSensorDto>>(json, options);
            return sensors ?? new List<AirSensorDto>();
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            Console.WriteLine($"Error retrieving sensor data: {ex.Message}");
            return new List<AirSensorDto>();
        }
    }
    // ✅ Find closest sensor
    public double? FindClosestSensor(List<AirSensorDto> sensors, double targetLat, double targetLon)
    {
        var result = sensors
            .OrderBy(s => HaversineDistance(targetLat, targetLon, s.Latitud, s.Longitud))
            .FirstOrDefault();
        return result != null ? result.IndexQualitatAire : null;
    }

    // ✅ Haversine formula to calculate great-circle distance between two points
    private double HaversineDistance(double lat1, double lon1, double lat2, double lon2)
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

    private double DegreesToRadians(double deg)
    {
        return deg * (Math.PI / 180);
    }
}
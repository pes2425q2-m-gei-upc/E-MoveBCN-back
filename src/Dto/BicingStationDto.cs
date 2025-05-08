using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Dto;

public class BicingStationDto
{
  [JsonPropertyName("station_id")]
  public required int BicingId { get; set; }
  [JsonPropertyName("name")]
  public required string BicingName { get; set; }
  [JsonPropertyName("lat")]
  public required double Latitude { get; set; }
  [JsonPropertyName("lon")]
  public required double Longitude { get; set; }
  [JsonPropertyName("altitude")]
  public required double Altitude { get; set; }
  [JsonPropertyName("address")]
  public required string Address { get; set; }
  [JsonPropertyName("cross_street")]
  public required string CrossStreet { get; set; }
  [JsonPropertyName("post_code")]
  public required string PostCode { get; set; }
  [JsonPropertyName("capacity")]
  public required int Capacity { get; set; }
  [JsonPropertyName("is_charging_station")]
  public required bool IsChargingStation { get; set; }
}

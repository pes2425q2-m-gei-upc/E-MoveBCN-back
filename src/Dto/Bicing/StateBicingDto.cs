#nullable enable
using System;
using System.Text.Json.Serialization;

namespace Dto.Bicing;

public class StateBicingDto
{

  [JsonPropertyName("station_id")]
  public required int BicingId { get; set; }
  [JsonPropertyName("num_bikes_available")]
  public required int NumBikesAvailable { get; set; }
  [JsonPropertyName("num_bikes_available_types.mechanical")]
  public required int NumBikesAvailableMechanical { get; set; }
  [JsonPropertyName("num_bikes_available_types.ebike")]
  public required int NumBikesAvailableEbike { get; set; }
  [JsonPropertyName("num_docks_available")]
  public required int NumDocksAvailable { get; set; }
  [JsonPropertyName("last_reported")]
  public required DateTime LastReported { get; set; }
  [JsonPropertyName("status")]
  public required string Status { get; set; }

  public BicingStationDto? BicingStation { get; set; }
}

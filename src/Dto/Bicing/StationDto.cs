#nullable enable
using System.Text.Json.Serialization;
namespace Dto.Bicing;
public class StationDto
{
  [JsonPropertyName("id")]
  public required string StationId { get; set; }
  [JsonPropertyName("label")]
  public string? StationLabel { get; set; }
  [JsonPropertyName("coordinates.latitude")]
  public required double StationLatitude { get; set; }
  [JsonPropertyName("coordinates.longitude")]
  public required double StationLongitude { get; set; }
  public required string LocationId { get; set; }
  public LocationDto? Location { get; set; }
}

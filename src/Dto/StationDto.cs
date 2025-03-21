using System.Collections.Generic;
using System.Text.Json.Serialization;

public class StationDto
{
    [JsonPropertyName("id")]
    public required string StationId { get; set; }
    [JsonPropertyName("label")]
    public required string StationLabel { get; set; }
    [JsonPropertyName("coordinates.latitude")]
    public required double StationLatitude { get; set; }
    [JsonPropertyName("coordinates.longitude")]
    public required double StationLongitude { get; set; }
    [JsonPropertyName("reservable")]
    public required bool Reservable { get; set; }
    public required string LocationId { get; set; }
    public LocationDto? Location { get; set; }    
}
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class SavedUbicationDto
{
    [JsonPropertyName("ubication_id")]
    public required Guid UbicationId { get; set; }

    [JsonPropertyName("user_id")]
    public required Guid UserId { get; set; }

    [JsonPropertyName("station_type")]
    public required string StationType { get; set; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; set; }
    public UserDto User { get; set; }
}
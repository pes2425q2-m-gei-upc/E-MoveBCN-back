using System;
using System.Collections.Generic;
using Dto;
using System.Text.Json.Serialization;

public class SavedUbicationDto
{
    [JsonPropertyName("ubication_id")]
    public required int UbicationId { get; set; }

    [JsonPropertyName("username")]
    public required string Username { get; set; }

    [JsonPropertyName("station_type")]
    public required string StationType { get; set; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; set; }
}
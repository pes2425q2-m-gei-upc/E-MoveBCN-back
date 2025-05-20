using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Dto;
namespace Dto;
public class SavedUbicationDto
{
  [JsonPropertyName("ubication_id")]
  public required int UbicationId { get; set; }

  [JsonPropertyName("username")]
  public required string UserEmail { get; set; }

  [JsonPropertyName("station_type")]
  public required string StationType { get; set; }

  [JsonPropertyName("latitude")]
  public required double Latitude { get; set; }

  [JsonPropertyName("longitude")]
  public required double Longitude { get; set; }
  public int? Valoration { get; set; }
  public string? Comment { get; set; }
  public double? AirQuality { get; set; }
}

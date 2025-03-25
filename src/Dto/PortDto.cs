using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PortDto
{
    [JsonPropertyName("id")]
    public required string PortId { get; set; }
    
    [JsonPropertyName("connector_type")]
    public required string ConnectorType { get; set; }
    
    [JsonPropertyName("power_kw")]
    public required double PowerKw { get; set; }
    
    [JsonPropertyName("charging_mechanism")]
    public required string ChargingMechanism { get; set; }
    
    [JsonPropertyName("port_status[0].status")]
    public required string PortStatus { get; set; }

    [JsonPropertyName("reservable")]
    public required bool Reservable { get; set; }
    
    [JsonPropertyName("last_updated")]
    public required DateTime LastUpdated { get; set; }
    
    public required string StationId { get; set; }
    public virtual StationDto Station { get; set; }
}
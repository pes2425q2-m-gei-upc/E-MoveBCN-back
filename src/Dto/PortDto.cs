using System;
using System.Collections.Generic;

public class PortDto
{
    public required string PortId { get; set; }
    public required string ConnectorType { get; set; }
    public required float PowerKw { get; set; }
    public required string ChargingMechanism { get; set; }
    public required string Status { get; set; }
    public required DateTime LastUpdated { get; set; }
    public required string StationId { get; set; }
    public virtual StationDto Station { get; set; }
}
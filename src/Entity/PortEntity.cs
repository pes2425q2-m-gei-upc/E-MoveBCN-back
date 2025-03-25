using System;

public class PortEntity 
{
   public required string PortId { get; set; }
    public required string ConnectorType { get; set; }
    public required double PowerKw { get; set; }
    public required string ChargingMechanism { get; set; }
    public required string Status { get; set; }
    public required DateTime LastUpdated { get; set; }
    public required bool Reservable { get; set; }
    public required string StationId { get; set; }
    public virtual StationEntity StationIdNavigation { get; set; }
}
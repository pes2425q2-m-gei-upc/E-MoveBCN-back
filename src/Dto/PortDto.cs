using System;
using System.Collections.Generic;

public class PortDto
{
    public string PortId { get; set; }
    public string ConnectorType { get; set; }
    public float PowerKw { get; set; }
    public string ChargingMechanism { get; set; }
    public string Status { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Notes { get; set; }
    public bool Reservable { get; set; }
    public List<AuthenticationDto> Authentications { get; set; }
}
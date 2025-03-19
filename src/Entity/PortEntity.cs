using System;
using System.Collections.Generic;

public class PortEntity 
{
   public string PortId { get; set; }
    public string ConnectorType { get; set; }
    public float PowerKw { get; set; }
    public string ChargingMechanism { get; set; }
    public string Status { get; set; }
    public DateTime LastUpdated { get; set; }
    public string Notes { get; set; }
    public bool Reservable { get; set; }
    
    public ICollection<AuthenticationEntity> Authentications { get; set; }
}
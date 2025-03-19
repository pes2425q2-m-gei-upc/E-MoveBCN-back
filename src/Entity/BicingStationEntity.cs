using System;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public class BicingStationEntity
{
    [Key]
    public int BicingId { get; set; }
    public string BicingName { get; set; }
    public float Latitude { get; set; }
    public float Longitude { get; set; }
    public float Altitude { get; set; }
    public string Address { get; set; }
    public string CrossStreet { get; set; }
    public string PostCode { get; set; }
    public int Capacity { get; set; }
    public bool IsChargingStation { get; set; }
    public int ShortName { get; set; }
    public float NearbyDistance { get; set; }
    public DateTime LastUpdated { get; set; }
    public bool RideCodeSupport { get; set; }
    
    public virtual StateBicingEntity State { get; set; }
} 
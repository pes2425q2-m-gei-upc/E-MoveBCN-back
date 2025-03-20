using System;
using System.ComponentModel.DataAnnotations;

namespace Entity;

public class BicingStationEntity
{
    [Key]
    public required int BicingId { get; set; }
    public required string BicingName { get; set; }
    public required float Latitude { get; set; }
    public required float Longitude { get; set; }
    public required float Altitude { get; set; }
    public required string Address { get; set; }
    public required string CrossStreet { get; set; }
    public required string PostCode { get; set; }
    public required int Capacity { get; set; }
    public required bool IsChargingStation { get; set; }
    public required DateTime LastUpdated { get; set; }
    } 
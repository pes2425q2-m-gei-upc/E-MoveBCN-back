using System;

public class LocationEntity
{
    public required string LocationId { get; set; }
    public required string NetworkBrandName { get; set; }
    public required string OperatorPhone { get; set; }
    public required string OperatorWebsite { get; set; }
    public required float Latitude { get; set; }
    public required float Longitude { get; set; }
    public required string AddressString { get; set; }
    public required string Locality { get; set; }
    public required string PostalCode { get; set; }
    public DateTime LastUpdated { get; set; }
}
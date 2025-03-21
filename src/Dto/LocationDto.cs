using System;
using System.Text.Json.Serialization;
public class LocationDto
{
    [JsonPropertyName("id")]
    public required string LocationId { get; set; }
    
    [JsonPropertyName("network_brand_name")]
    public required string NetworkBrandName { get; set; }
    
    [JsonPropertyName("contact.operator_phone")]
    public required string OperatorPhone { get; set; }
    
    [JsonPropertyName("contact.operator_website")]
    public required string OperatorWebsite { get; set; }
    
    [JsonPropertyName("coordinates.latitude")]
    public double Latitude { get; set; }
    
    [JsonPropertyName("coordinates.longitude")]
    public double Longitude { get; set; }
    
    [JsonPropertyName("address.address_string")]
    public required string AddressString { get; set; }
    
    [JsonPropertyName("address.locality")]
    public required string Locality { get; set; }
    
    [JsonPropertyName("address.postal_code")]
    public required string PostalCode { get; set; }
    
    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }
}
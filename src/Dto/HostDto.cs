using System.Collections.Generic;
using System.Text.Json.Serialization;
public class HostDto
{
    public required string HostId { get; set; }

    [JsonPropertyName("name")]
    public required string HostName { get; set; }
    
    [JsonPropertyName("address.address_string")]
    public required string HostAddress { get; set; }
    
    [JsonPropertyName("address.locality")]
    public required string HostLocality { get; set; }

    [JsonPropertyName("address.postal_code")]
    public required string HostPostalCode { get; set; }
    
    [JsonPropertyName("contact.operator_phone")]
    public required string OperatorPhone { get; set; }

    [JsonPropertyName("contact.operator_website")]
    public required string OperatorWebsite { get; set; }
    
    public required string LocationId { get; set; }
    public LocationDto? Location { get; set; }
}
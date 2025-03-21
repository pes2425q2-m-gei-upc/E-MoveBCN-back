using System.Collections.Generic;

public class HostDto
{
    public required string HostId { get; set; }
    public required string HostName { get; set; }
    public required string HostAddress { get; set; }
    public required string HostLocality { get; set; }
    public required string HostPostalCode { get; set; }
    public required string OperatorPhone { get; set; }
    public required string OperatorWebsite { get; set; }
    public required string LocationId { get; set; }
    public LocationDto? Location { get; set; }
}
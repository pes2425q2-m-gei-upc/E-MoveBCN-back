using System.Collections.Generic;

public class StationDto
{
    public required string StationId { get; set; }
    public required string StationLabel { get; set; }
    public required float StationLatitude { get; set; }
    public required float StationLongitude { get; set; }
    public required bool Reservable { get; set; }
    public required string LocationId { get; set; }
    public LocationDto? Location { get; set; }    
}
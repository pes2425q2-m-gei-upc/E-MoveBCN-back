public class StationEntity
{
    
    public required string StationId { get; set; } 
    public required string StationLabel { get; set; }
    public required float StationLatitude { get; set; }
    public required float StationLongitude { get; set; }
    public required bool Reservable { get; set; }
    public required string LocationId { get; set; }
    public LocationEntity? LocationIdNavigation { get; set; } 
}

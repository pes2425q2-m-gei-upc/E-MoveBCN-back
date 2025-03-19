public class StationEntity
{
    
    public string StationId { get; set; } 
    public string StationLabel { get; set; }
    public float StationLatitude { get; set; }
    public float StationLongitude { get; set; }
    
    public string LocationId { get; set; }
    public LocationEntity Location { get; set; } 
}


public class StationEntity
{

  public required string StationId { get; set; }
  public string StationLabel { get; set; }
  public required double StationLatitude { get; set; }
  public required double StationLongitude { get; set; }
  public required string LocationId { get; set; }
  public LocationEntity? LocationIdNavigation { get; set; }
}

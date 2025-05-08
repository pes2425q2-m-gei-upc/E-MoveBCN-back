public class RouteRequestDto
{
  public double OriginLat { get; set; }
  public double OriginLng { get; set; }
  public double DestinationLat { get; set; }
  public double DestinationLng { get; set; }
  public string Mode { get; set; }            // "car", "bike", "walk", "transit"
  public string Preference { get; set; } = "fastest";
}

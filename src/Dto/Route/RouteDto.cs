using System.Collections.Generic;
using Dto.User;

namespace Dto.Route;
public class RouteDto
{
  public string RouteId { get; set; }
  public double OriginLat { get; set; }
  public double OriginLng { get; set; }
  public double DestinationLat { get; set; }
  public double DestionationLng { get; set; }
  public string mean { get; set; } // "car", "bike", "walk", "transit"
  public string Preference { get; set; } = "fastest";
  public double Distance { get; set; }
  public double Duration { get; set; }
  public List<double[]> Geometry { get; set; }
  public List<RouteInstructionDto> Instructions { get; set; }
  public required string OriginStreetName { get; set; }
  public required string DestinationStreetName { get; set; }
  public string UserId { get; set; }
  public UserDto User { get; set; }
}

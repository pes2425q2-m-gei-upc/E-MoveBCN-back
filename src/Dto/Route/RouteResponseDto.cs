using System.Collections.Generic;
namespace src.Dto.Route;
public class RouteResponseDto
{
  public string RouteId { get; set; }
  public double Distance { get; set; }
  public double Duration { get; set; }
  public List<double[]> Geometry { get; set; } = new();
  public List<RouteInstructionDto> Instructions { get; set; }
  public string? OriginStreetName { get; set; }
  public string? DestinationStreetName { get; set; }
}

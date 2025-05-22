using System;
namespace Dto.Route;
public class PublishedRouteDto
{
  public string RouteId { get; set; }

  public DateTime Date { get; set; }

  public int AvailableSeats { get; set; }

  public RouteDto Route { get; set; }
}

#nullable enable
using System;
namespace Entity.Route;

public class PublishedRouteEntity
{
  public required Guid RouteId { get; set; }

  public required DateTime Date { get; set; }

  public required int AvailableSeats { get; set; }

  public virtual RouteEntity? RouteIdNavigation { get; set; }
}

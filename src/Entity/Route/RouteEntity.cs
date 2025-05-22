using System;
using Entity.User;
namespace Entity.Route;
public class RouteEntity
{
  public required Guid RouteId { get; set; }
  public required double OriginLat { get; set; }
  public required double OriginLng { get; set; }
  public required double DestinationLat { get; set; }
  public required double DestinationLng { get; set; }
  public required string Mean { get; set; }
  public required string Preference { get; set; }
  public required float Distance { get; set; }
  public required float Duration { get; set; }
  public required string GeometryJson { get; set; }
  public required string InstructionsJson { get; set; }
  public required string OriginStreetName { get; set; }
  public required string DestinationStreetName { get; set; }
  public required Guid UserId { get; set; }
  public virtual UserEntity UserIdNavigation { get; set; } = null!;
}

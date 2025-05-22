#nullable enable
using System;
namespace Entity.Bicing;
public class HostEntity
{
  public required Guid HostId { get; set; }
  public required string HostName { get; set; }
  public required string HostAddress { get; set; }
  public required string HostLocality { get; set; }
  public required string HostPostalCode { get; set; }
  public required string OperatorPhone { get; set; }
  public required string OperatorWebsite { get; set; }
  public required string LocationId { get; set; }
  public LocationEntity? LocationIdNavigation { get; set; }
}

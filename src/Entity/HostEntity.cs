using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class HostEntity
{
  public required Guid HostId { get; set; }
  public required string HostName { get; set; }
  public required string HostAddress { get; set; }
  public required string HostLocality { get; set; }
  public required string HostPostalCode { get; set; }
  public required string OperatorPhone { get; set; }
  public required string OperatorWebsite { get; set; }
  public string LocationId { get; set; }
  public LocationEntity? LocationIdNavigation { get; set; }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity;

public class StateBicingEntity
{
  public required int BicingId { get; set; }
  public required int NumBikesAvailable { get; set; }
  public required int NumBikesAvailableMechanical { get; set; }
  public required int NumBikesAvailableEbike { get; set; }
  public required int NumDocksAvailable { get; set; }
  public required DateTime LastReported { get; set; }
  public required string Status { get; set; }
  public virtual BicingStationEntity BicingStationIdNavigation { get; set; }
}

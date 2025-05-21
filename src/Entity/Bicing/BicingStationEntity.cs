using System.ComponentModel.DataAnnotations;
namespace Entity.Bicing;
public class BicingStationEntity
{
  [Key]

  public required int BicingId { get; set; }
  public required string BicingName { get; set; }
  public required double Latitude { get; set; }
  public required double Longitude { get; set; }
  public required double Altitude { get; set; }
  public required string Address { get; set; }
  public required string CrossStreet { get; set; }
  public required string PostCode { get; set; }
  public required int Capacity { get; set; }
  public required bool IsChargingStation { get; set; }
}

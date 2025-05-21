namespace Dto.Ubication;

public class UbicationDetailWithAir
{
  public required object Detail { get; set; } = default!;
  public required double? AirQualityIndex { get; set; }
}

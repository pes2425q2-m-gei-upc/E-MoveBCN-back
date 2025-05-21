
namespace Dto.Bicing;
public class BicingStationWithStatusDto
{
  public BicingStationDto StationInfo { get; set; } = default!;
  public StateBicingDto RealTimeStatus { get; set; } = default!;
}

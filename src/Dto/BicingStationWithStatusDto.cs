using Dto;
namespace src.Dto;
public class BicingStationWithStatusDto
{
    public BicingStationDto StationInfo { get; set; } = default!;
    public StateBicingDto RealTimeStatus { get; set; } = default!;
}
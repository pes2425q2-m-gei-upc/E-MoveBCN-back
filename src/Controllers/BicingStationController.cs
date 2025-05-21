using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Bicing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/bicingstation
[Authorize]
public class BicingStationController(IBicingStationService bicingStationService, IStateBicingService stateBicingService, IMapper mapper) : ControllerBase
{
  private readonly IBicingStationService _bicingStationService = bicingStationService;
  private readonly IStateBicingService _stateBicingService = stateBicingService;

  private readonly IMapper _mapper = mapper;

  [HttpGet("bicingstations")] // api/BicingStation/bicingstations
  public async Task<IActionResult> GetAllStations()
  {
    var stations = await this._bicingStationService.GetAllBicingStations().ConfigureAwait(false);
    var states = await this._stateBicingService.GetAllStateBicingStationsAsync().ConfigureAwait(false);

    var combinedData = stations.Join(
        states,
        station => station.BicingId,
        state => state.BicingId,
        (station, state) => new CombinedBicingDto
        {
          StationInfo = this._mapper.Map<BicingStationDto>(station),
          RealTimeStatus = this._mapper.Map<StateBicingDto>(state)
        }
    ).ToList();

    return Ok(combinedData);
  }
}

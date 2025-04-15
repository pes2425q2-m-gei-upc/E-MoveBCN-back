using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using Entity;
using Dto;
using AutoMapper;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/bicingstation
[Authorize]
public class BicingStationController : ControllerBase
{
    private readonly IBicingStationService _bicingStationService;
    private readonly IStateBicingService _stateBicingService;

    private readonly IMapper _mapper;

    public BicingStationController(IBicingStationService bicingStationService, IStateBicingService stateBicingService, IMapper mapper)
    {
        _bicingStationService = bicingStationService;
        _mapper = mapper;
        _stateBicingService = stateBicingService;
    }
   
    [HttpGet("bicingstations")] // api/BicingStation/bicingstations
    public async Task<IActionResult> GetAllStations()
    {
        var stations = await _bicingStationService.GetAllBicingStationsAsync();
        var states = await _stateBicingService.GetAllStateBicingStationsAsync();

        var combinedData = stations.Join(
            states,
            station => station.BicingId,
            state => state.BicingId,
            (station, state) => new CombinedBicingDto
            {
                StationInfo = _mapper.Map<BicingStationDto>(station),
                RealTimeStatus = _mapper.Map<StateBicingDto>(state)
            }
        ).ToList();

        return Ok(combinedData);
    }
}
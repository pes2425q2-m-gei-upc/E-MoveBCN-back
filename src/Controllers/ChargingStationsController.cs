using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/ChargingStations
[Authorize]
public class ChargingStationsController : ControllerBase
{
    private readonly IChargingStationsService _chargingStationService;

    public ChargingStationsController(IChargingStationsService dadesObertesService)
    {
        _chargingStationService = dadesObertesService;
    }
   
    [HttpGet("stations")] // api/ChargingStations/stations
    public async Task<IActionResult> GetAllStations()
    {
        var stations = await _chargingStationService.GetAllChargingStationsAsync();
        return Ok(stations);
    }
}

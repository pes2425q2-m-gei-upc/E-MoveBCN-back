using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
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
    var stations = await _chargingStationService.GetAllChargingStationsAsync().ConfigureAwait(false);
    return Ok(stations);
  }
}

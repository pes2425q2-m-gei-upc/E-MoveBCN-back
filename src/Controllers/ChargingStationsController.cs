using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/ChargingStations
[Authorize]
public class ChargingStationsController(IChargingStationsService dadesObertesService) : ControllerBase
{
  private readonly IChargingStationsService _chargingStationService = dadesObertesService;

  [HttpGet("stations")] // api/ChargingStations/stations
  public async Task<IActionResult> GetAllStations()
  {
    var stations = await this._chargingStationService.GetAllChargingStationsAsync().ConfigureAwait(false);
    return Ok(stations);
  }
}

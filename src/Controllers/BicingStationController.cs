using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/bicingstation
//[Authorize]
public class BicingStationController : ControllerBase
{
    private readonly IBicingStationService _bicingStationService;

    public BicingStationController(IBicingStationService bicingStationService)
    {
        _bicingStationService = bicingStationService;
    }
   
    [HttpGet("bicingstations")] // api/bicingstation/stations
    public async Task<IActionResult> GetAllStations()
    {
        var stations = await _bicingStationService.GetAllBicingStationsAsync();
        return Ok(stations);
    }
}
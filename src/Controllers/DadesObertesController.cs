using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Services.Interface;
namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/dadesobertes
//[Authorize]
public class DadesObertesController : ControllerBase
{
    private readonly IDadesObertesService _dadesObertesService;

    public DadesObertesController(IDadesObertesService dadesObertesService)
    {
        _dadesObertesService = dadesObertesService;
    }
   
    [HttpGet("stations")] // api/dadesobertes/stations
    public async Task<IActionResult> GetAllStations()
    {
        var stations = await _dadesObertesService.GetAllStationsAsync();
        return Ok(stations);
    }
}

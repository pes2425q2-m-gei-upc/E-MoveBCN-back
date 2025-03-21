using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Services;
using Entity;
namespace Controllers;


 [ApiController]
[Route("api/[controller]")]
public class DadesObertesController : ControllerBase
{
    private readonly DadesObertesService _dadesObertesService;

    public DadesObertesController(DadesObertesService dadesObertesService)
    {
        _dadesObertesService = dadesObertesService;
    }
   
    [HttpGet("stations")]
    public async Task<IActionResult> GetAllStations()
    {
        var stations = await _dadesObertesService.GetAllStationsAsync();
        return Ok(stations);
    }
}

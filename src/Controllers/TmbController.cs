using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace Controllers;

[Route("api/[controller]")]  // Base route: api/tmb
[ApiController]
//[Authorize]
public class TmbController : ControllerBase
{
    private readonly ITmbService _tmbService;

    public TmbController(ITmbService tmbService)
    {
        _tmbService = tmbService;
    }

    // GET: /api/tmb/metros
    [HttpGet("Metros")]
    public async Task<IActionResult> GetAllMetros()
    {
        var metros = await _tmbService.GetAllMetrosAsync();
        return Ok(metros);
    }
    // GET: /api/tmb/bus
    [HttpGet("Bus")]
    public async Task<IActionResult> GetAllBus()
    {
        var bus = await _tmbService.GetAllBusAsync();
        return Ok(bus);
    }
}
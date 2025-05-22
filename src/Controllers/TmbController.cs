using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;
[Route("api/[controller]")]  // Base route: api/tmb
[ApiController]
[Authorize]
public class TmbController(ITmbService tmbService) : ControllerBase
{
  private readonly ITmbService _tmbService = tmbService;

  // GET: /api/tmb/metros
  [HttpGet("Metros")]
  public async Task<IActionResult> GetAllMetros()
  {
    var metros = await this._tmbService.GetAllMetrosAsync().ConfigureAwait(false);
    return Ok(metros);
  }
  // GET: /api/tmb/bus
  [HttpGet("Bus")]
  public async Task<IActionResult> GetAllBus()
  {
    var bus = await this._tmbService.GetAllBusAsync().ConfigureAwait(false);
    return Ok(bus);
  }
}

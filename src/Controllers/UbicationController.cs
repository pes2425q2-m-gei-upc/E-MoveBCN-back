using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plantilla.Web.src.Services.Interface;
using Services.Interface;

namespace plantilla.Web.src.Controllers;


[ApiController]
[Route("api/[controller]")] // api/ubication
[Authorize]
public class UbicationController(IUbicationService ubicationService) : ControllerBase
{

  private readonly IUbicationService _ubicationService = ubicationService;

  [HttpGet("getsavedubications")] // api/ubication/getsavedubications
  public async Task<IActionResult> GetAllSavedUbications([FromBody] string username)
  {
    if (string.IsNullOrEmpty(username))
    {
      return BadRequest("User ID is required.");
    }
    var savedUbications = await _ubicationService.GetUbicationsByUserIdAsync(username);
    if (savedUbications == null || savedUbications.Count == 0)
    {
      return NotFound("No saved ubications found for this user.");
    }
    return Ok(savedUbications);
  }
  [HttpPost("saveubication")] // api/ubication/saveubication
  public async Task<IActionResult> SaveUbication([FromBody] SavedUbicationDto savedUbication)
  {
    if (savedUbication == null)
    {
      return BadRequest("Saved ubication data is required.");
    }

    var result = await _ubicationService.SaveUbicationAsync(savedUbication);
    if (result == false)
    {
      return BadRequest("Failed to save ubication.");
    }
    return Ok("Saved ubication successfully.");
  }
}
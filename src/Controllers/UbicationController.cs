using System;
using System.Threading.Tasks;
using Constants;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plantilla.Web.src.Services.Interface;
using src.Dto;

namespace plantilla.Web.src.Controllers;


[ApiController]
[Route("api/[controller]")] // api/ubication
[Authorize]
public class UbicationController(IUbicationService ubicationService) : ControllerBase
{

  private readonly IUbicationService _ubicationService = ubicationService;

  [HttpGet("getsavedubications")]
  public async Task<IActionResult> GetAllSavedUbications([FromQuery] string userEmail)
  {
    if (string.IsNullOrEmpty(userEmail))
    {
      return BadRequest("User email is required.");
    }

    var savedUbications = await _ubicationService.GetUbicationsByUserIdAsync(userEmail).ConfigureAwait(false);

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
    var result = await _ubicationService.SaveUbicationAsync(savedUbication).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Failed to save ubication.");
    }
    return Ok("Saved ubication successfully.");
  }

  [HttpDelete("deleteubication")] // api/ubication/deleteubication
  public async Task<IActionResult> DeleteUbication([FromBody] UbicationInfoDto ubicationDeleteDto)
  {
    if (ubicationDeleteDto == null)
    {
      return BadRequest("Ubication data is required.");
    }
    var done = await _ubicationService.DeleteUbication(ubicationDeleteDto).ConfigureAwait(false);
    if (done == false)
    {
      return BadRequest("Failed to delete ubication.");
    }
    return Ok("Ubication deleted successfully.");

  }
  [HttpPost("valorate")] // api/ubication/valorate
  public async Task<IActionResult> Valorate([FromBody] UbicationInfoDto ubicationInfoDto)
  {
    if (ubicationInfoDto == null)
    {
      return BadRequest("Ubication data is required.");
    }
    if (ubicationInfoDto.Valoration == null)
    {
      return BadRequest("Valoration is required.");
    }
    if (ubicationInfoDto.Valoration < 1 || ubicationInfoDto.Valoration > 5)
    {
      return BadRequest("Valoration must be between 1 and 5.");
    }
    var result = await _ubicationService.UpdateUbication(ubicationInfoDto).ConfigureAwait(false);
    if (result == false)
    {
      return BadRequest("Failed to valorate ubication.");
    }
    return Ok("Ubication valorated successfully.");
  }
  [HttpGet("details")]
  public async Task<IActionResult> GetUbicationDetails(
      [FromQuery] int ubicationId,
      [FromQuery] string stationType)
  {
    var result = await _ubicationService.GetUbicationDetails(ubicationId, stationType).ConfigureAwait(false);
    if (result.Item1 == null && result.Item2 == null)
    {
      return NotFound("Ubication not found or invalid type.");
    }
    UbicationDetailWithAir ubicationDetailWithAir = new UbicationDetailWithAir
    {
      Detail = result.Item1,
      AirQualityIndex = result.Item2
    };
    return Ok(ubicationDetailWithAir);
  }
}

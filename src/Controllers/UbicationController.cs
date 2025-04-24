using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

namespace Controllers;


[ApiController]
[Route("api/[controller]")] // api/ubication
[Authorize]
public class UbicationController : ControllerBase
{
    
        private readonly IUbicationService _ubicationService;
    
        public UbicationController(IUbicationService ubicationService)
        {
            _ubicationService = ubicationService;
        }
    
        [HttpGet("savedubications")] // api/ubication/savedubications
        public async Task<IActionResult> GetAllSavedUbications([FromBody] string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("User ID is required.");
            }
            var savedUbications = await _ubicationService.GetUbicationsByUserIdAsync(userId);
            if (savedUbications == null || !savedUbications.Any())
            {
                return NotFound("No saved ubications found for this user.");
            }
            return Ok(savedUbications);
        }
}
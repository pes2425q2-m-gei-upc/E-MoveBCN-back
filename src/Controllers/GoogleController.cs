using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Claims;
using Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
namespace Controllers;



[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

     private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("login")]
    public IActionResult Login()
    {
        var redirectUrl = Url.Action("GoogleResponse", "Auth");
        var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("GoogleResponse")]
    public async Task<IActionResult> GoogleResponse()
    {
        var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        if (!result.Succeeded)
        {
            return Unauthorized(new
            {
                message = "Google authentication failed.",
                error = result.Failure?.Message ?? "No details available."
            });
        }

        if (result.Principal == null || !result.Principal.Identities.Any())
        {
            return Unauthorized(new
            {
                message = "Authentication succeeded, but no principal identities found."
            });
        }

        var email = result.Principal.FindFirstValue(ClaimTypes.Email);
        var name = result.Principal.FindFirstValue(ClaimTypes.Name);

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new
            {
                message = "Email claim not found in Google response."
            });
        }

        var user = await _userService.LoginWithGoogleAsync(name, email);

        // Aquí ya puedes usar el email y el name como necesites
        return Ok(new { email, name });
    }
}

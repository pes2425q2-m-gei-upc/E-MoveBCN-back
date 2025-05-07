using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Dto;
using Helpers.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]

public class AuthorizationController : ControllerBase
{
  private readonly IUserService _userService;
  private readonly IAuthenticationHelper _authenticationHelper;
  public AuthorizationController(IUserService userService, IAuthenticationHelper authenticationHelper)
  {
    _userService = userService;
    _authenticationHelper = authenticationHelper;
  }

  // POST: /api/authorization/login
  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] UserCredentials userCredentials)
  {
    if (userCredentials == null)
    {
      return BadRequest("Invalid data");
    }

    try
    {
      var user = await _userService.Authenticate(userCredentials).ConfigureAwait(false);
      if (user == null)
      {
        return Unauthorized("Invalid credentials");
      }
      var (claimsIdentity, authenticationProperties) = _authenticationHelper.AuthenticationClaims(user);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties).ConfigureAwait(false);
      return Ok("Authorized");
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Error en el servidor: {ex.Message}");
    }
  }

  // POST: /api/user/googlelogin
  [HttpPost("googlelogin")]
  public async Task<IActionResult> GoogleLogin([FromBody] LoginGoogleDto dto)
  {
    if (string.IsNullOrEmpty(dto.Email) || string.IsNullOrEmpty(dto.Username))
      return BadRequest("Invalid Data");

    try
    {
      var user = await _userService.LoginWithGoogleAsync(dto).ConfigureAwait(false);
      var (claimsIdentity, authenticationProperties) = _authenticationHelper.AuthenticationClaims(user);
      await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties).ConfigureAwait(false);
      return Ok(user);
    }
    catch (Exception ex)
    {
      return StatusCode(500, $"Server error: {ex.Message}");
    }
  }
}

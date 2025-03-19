using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Services.Interface;
using Dto;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Helpers.Interface;
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
            var user = await _userService.Authenticate(userCredentials);
            if(user == null)
            {
                return Unauthorized("Invalid credentials");
            }
            var (claimsIdentity, authenticationProperties) = _authenticationHelper.AuthenticationClaims(user);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authenticationProperties);
            return Ok("Authorized");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor: {ex.Message}");
        }
    }
}
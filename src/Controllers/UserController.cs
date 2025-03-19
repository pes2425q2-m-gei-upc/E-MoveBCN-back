using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using BCrypt.Net;


namespace Controllers;

[Route("api/[controller]")]  // Base route: api/user
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: /api/user/getuserstest
    [HttpGet("getuserstest")]  // Solo ruta adicional
    public IActionResult GetUserstest()
    {
        var users = new[] {
            new { Id = 1, Name = "Juan", Email = "juan@example.com", PasswordHash = "password123", Idioma = "es" },
            new { Id = 2, Name = "Ana", Email = "ana@example.com", PasswordHash = "password456", Idioma = "en" }
        };

        return Ok(users);
    }

    // GET: /api/user/getusers
    [HttpGet("getusers")]  // Solo ruta adicional
    public IActionResult GetUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
}

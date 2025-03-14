using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;

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
            new { Id = 1, Name = "Juan" },
            new { Id = 2, Name = "Ana" }
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

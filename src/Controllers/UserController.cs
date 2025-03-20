using Microsoft.AspNetCore.Mvc;
using Services.Interface;
using Dto;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;


namespace Controllers;

[Route("api/[controller]")]  // Base route: api/user
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: /api/user/getuserstest
    [HttpGet("getuserstest")]
    public IActionResult GetUserstest()
    {
        var users = new[] {
            new { Id = 1, Name = "Juan", Email = "juan@example.com", PasswordHash = "password123", Idioma = "es" },
            new { Id = 2, Name = "Ana", Email = "ana@example.com", PasswordHash = "password456", Idioma = "en" }
        };

        return Ok(users);
    }

    //POST: /api/user/createuser
    [HttpPost("createuser")]
    public IActionResult CreateUser([FromBody] UserCreate user)
    {
        if (user == null)
        {
            return BadRequest("Invalid data");
        }

        try
        {
            var isCreated = _userService.CreateUser(user);

            if (isCreated)
            {
                return Ok("User created successfully");
            }
            return BadRequest("Error");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor: {ex.Message}");
        }
    }
    //DELETE /api/user/deleteuser
    [HttpDelete("deleteuser")]
    public async Task<IActionResult> DeleteUser([FromBody] UserCredentials user)
    {
        if (user == null)
        {
            return BadRequest("Invalid data");
        }

        try
        {
            var isDeleted = await _userService.DeleteUser(user);

            if (isDeleted)
            {
                return Ok("User deleted successfully");
            }
            return BadRequest("Incorrect credentials");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor: {ex.Message}");
        }
    }
    // GET: /api/user/getusers
    [HttpGet("getusers")]
    public IActionResult GetUsers()
    {
        var users = _userService.GetAllUsers();
        return Ok(users);
    }
}

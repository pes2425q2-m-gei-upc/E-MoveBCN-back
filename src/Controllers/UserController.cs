using System;
using System.Threading.Tasks;
using Dto.Chat;
using Dto.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;
[Route("api/[controller]")]  // Base route: api/user
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ControllerBase
{
  private readonly IUserService _userService = userService;

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
  [AllowAnonymous]
  public IActionResult CreateUser([FromBody] UserCreate user)
  {
    if (user == null)
    {
      return BadRequest("Invalid data");
    }

    try
    {
      var isCreated = this._userService.CreateUser(user);

      if (isCreated)
      {
        return Ok("User created successfully");
      }
      return BadRequest("Error");
    }
    catch (ArgumentException ex)
    {
      return BadRequest($"Argument error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
      return StatusCode(500, $"Operation error: {ex.Message}");
    }
    catch (Exception)
    {
      // Optionally log the exception here
      throw; // Rethrow to let the framework handle unexpected exceptions
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
      var isDeleted = await this._userService.DeleteUser(user).ConfigureAwait(false);

      if (isDeleted)
      {
        return Ok("User deleted successfully");
      }
      return BadRequest("Incorrect credentials");
    }
    catch (ArgumentException ex)
    {
      return BadRequest($"Argument error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
      return StatusCode(500, $"Operation error: {ex.Message}");
    }
    catch (Exception)
    {
      // Optionally log the exception here
      throw; // Rethrow to let the framework handle unexpected exceptions
    }
  }
  // GET: /api/user/getusers
  [HttpGet("getusers")]
  public IActionResult GetUsers()
  {
    var users = this._userService.GetAllUsers();
    return Ok(users);
  }
  // POST: /api/user/modify
  [HttpPost("modify")]
  public async Task<IActionResult> ModifyUser([FromBody] UserDto user)
  {
    if (user == null)
    {
      return BadRequest("Invalid data");
    }

    try
    {
      var isModified = await this._userService.ModifyUser(user).ConfigureAwait(false);

      if (isModified)
      {
        return Ok("User modified successfully");
      }
      return BadRequest("Incorrect credentials");
    }
    catch (ArgumentException ex)
    {
      return BadRequest($"Argument error: {ex.Message}");
    }
    catch (InvalidOperationException ex)
    {
      return StatusCode(500, $"Operation error: {ex.Message}");
    }
    catch (Exception)
    {
      // Optionally log the exception here
      throw; // Rethrow to let the framework handle unexpected exceptions
    }
  }

  [HttpPost("block")] // api/user/block
  public async Task<IActionResult> BlockUser([FromBody] BlockRequestDto request)
  {
    var result = await this._userService.BlockUserAsync(request).ConfigureAwait(false);
    return result ? Ok("Usuario bloqueado") : BadRequest("Ya estaba bloqueado o error");
  }

  [HttpPost("unblock")] // api/user/unblock
  public async Task<IActionResult> UnblockUser([FromBody] BlockRequestDto request)
  {
    var result = await this._userService.UnblockUserAsync(request).ConfigureAwait(false);
    return result ? Ok("Usuario desbloqueado") : NotFound("No estaba bloqueado");
  }

}

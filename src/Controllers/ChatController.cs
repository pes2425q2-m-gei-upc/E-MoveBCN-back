using System;
using System.Threading.Tasks;
using Dto.Chat;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController(IChatService chatService) : ControllerBase
{
  private readonly IChatService _chatService = chatService;

  [HttpPost("create")]
  public async Task<IActionResult> CreateChat([FromBody] ChatRequestDto request)
  {
    try
    {
      var created = await this._chatService.CreateChatAsync(request).ConfigureAwait(false);
      return created ? Ok("Chat creado") : StatusCode(500, "No se pudo crear el chat");
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
    catch
    {
      throw;
    }
  }

  [HttpDelete("deletechat")]
  public async Task<IActionResult> DeleteChat([FromBody] DeleteChatRequestDto request)
  {
    if (request == null || request.ChatId == Guid.Empty)
    {
      return BadRequest("Datos inválidos");
    }

    try
    {
      var deleted = await this._chatService.DeleteChatAsync(request.ChatId).ConfigureAwait(false);

      if (deleted)
      {
        return Ok("Chat eliminado correctamente");
      }

      return NotFound("Chat no encontrado");
    }
    catch (ArgumentException ex)
    {
      return BadRequest(ex.Message);
    }
    catch
    {
      throw;
    }
  }

}

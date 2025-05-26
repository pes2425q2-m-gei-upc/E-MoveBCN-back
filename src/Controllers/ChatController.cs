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
      var chatId = await this._chatService.CreateChatAsync(request);
      if (chatId == null)
        return StatusCode(500, "No se pudo crear el chat.");

      return Ok(chatId);
    }
    catch (UnauthorizedAccessException)
    {
      return StatusCode(403, "Usuarios bloqueados entre sí.");
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
  
  [HttpGet("getchats")]  // api/chat/getchats?userId=...
  public async Task<IActionResult> GetChatsForUser([FromQuery] Guid userId)
  {
      if (userId == Guid.Empty)
          return BadRequest("El parámetro userId es obligatorio.");

      var chats = await _chatService.GetChatsForUserAsync(userId).ConfigureAwait(false);
      return Ok(chats);
  }

}

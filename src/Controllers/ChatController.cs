using System.Threading.Tasks;
using Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services;
using Dto;
using Services.Interface;
using System;
namespace Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IChatService _chatService;

    public ChatController(IChatService chatService)
    {
        _chatService = chatService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateChat([FromBody] ChatRequestDto request)
    {
        try
        {
            var created = await _chatService.CreateChatAsync(request);
            return created ? Ok("Chat creado") : StatusCode(500, "No se pudo crear el chat");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
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
            var deleted = await _chatService.DeleteChatAsync(request.ChatId).ConfigureAwait(false);

            if (deleted)
            {
                return Ok("Chat eliminado correctamente");
            }

            return NotFound("Chat no encontrado");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error en el servidor: {ex.Message}");
        }
    }

}

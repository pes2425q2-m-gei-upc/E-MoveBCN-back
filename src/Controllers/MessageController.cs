using System;
using System.Threading.Tasks;
using Dto.Chat;
using Microsoft.AspNetCore.Mvc;
using Services.Interface;
namespace Controllers;

[ApiController]
[Route("api/messages")]
public class MessageController(IMessageService messageService) : ControllerBase
{
  private readonly IMessageService _messageService = messageService;

  [HttpPost("send")]
  public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
  {
    var result = await this._messageService.SendMessageAsync(dto).ConfigureAwait(false);
    return Ok(result);
  }

  [HttpGet("getMessages/{chatId}")]
  public async Task<IActionResult> GetMessages(Guid chatId)
  {
    var messages = await this._messageService.GetMessagesByChatIdAsync(chatId).ConfigureAwait(false);
    return Ok(messages);
  }

  [HttpGet("between")]
  public async Task<IActionResult> GetMessagesBetween(
    [FromQuery] Guid chatId,
    [FromQuery] DateTime from,
    [FromQuery] DateTime to)
  {
      var messages = await _messageService.GetMessagesBetweenAsync(chatId, from, to);
      return Ok(messages);
  }

  [HttpGet("last")]
  public async Task<IActionResult> GetLastMessages(
    [FromQuery] Guid chatId,
    [FromQuery] int count)
  {
      var messages = await _messageService.GetLastMessagesAsync(chatId, count);
      return Ok(messages);
  }
}

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
}

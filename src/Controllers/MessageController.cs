using System;
using System.Threading.Tasks;
using Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/messages")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMessage([FromBody] SendMessageDto dto)
    {
        var result = await _messageService.SendMessageAsync(dto);
        return Ok(result);
    }

    [HttpGet("getMessages/{chatId}")]
    public async Task<IActionResult> GetMessages(Guid chatId)
    {
        var messages = await _messageService.GetMessagesByChatIdAsync(chatId);
        return Ok(messages);
    }
}

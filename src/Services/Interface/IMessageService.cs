using Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMessageService
{
    Task<MessageDto> SendMessageAsync(SendMessageDto dto);
    Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
}

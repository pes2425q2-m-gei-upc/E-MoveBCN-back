using Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IMessageRepository
{
    Task<MessageDto> CreateMessageAsync(MessageEntity message);
    Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
}
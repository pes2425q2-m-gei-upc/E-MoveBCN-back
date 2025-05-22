using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Chat;
using Entity;
using Entity.Chat;
namespace Repositories.Interface;
public interface IMessageRepository
{
  Task<MessageDto> CreateMessageAsync(MessageEntity message);
  Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
}

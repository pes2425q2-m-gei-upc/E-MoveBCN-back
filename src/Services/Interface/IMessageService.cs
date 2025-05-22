using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Chat;
namespace Services.Interface;
public interface IMessageService
{
  Task<MessageDto> SendMessageAsync(SendMessageDto dto);
  Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId);
}

using System;
using System.Threading.Tasks;
using Dto.Chat;
namespace Services.Interface;
public interface IChatService
{
  Task<bool> CreateChatAsync(ChatRequestDto request);
  Task<bool> DeleteChatAsync(Guid chatId);
}

using System;
using System.Threading.Tasks;
using Dto.Chat;
namespace Services.Interface;
public interface IChatService
{
  Task<string> CreateChatAsync(ChatRequestDto request);
  Task<bool> DeleteChatAsync(Guid chatId);
}

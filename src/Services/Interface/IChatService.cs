using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dto.Chat;
namespace Services.Interface;

public interface IChatService
{
  Task<Guid?> CreateChatAsync(ChatRequestDto request);
  Task<bool> DeleteChatAsync(Guid chatId);
  Task<List<ChatResponseDto>> GetChatsForUserAsync(Guid userId);
}

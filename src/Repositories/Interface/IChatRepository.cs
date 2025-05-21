#nullable enable
using System;
using System.Threading.Tasks;
using Dto.Chat;
using Entity.Chat;
namespace Repositories.Interface;
public interface IChatRepository
{
  Task<bool> CreateChatAsync(ChatEntity chat);

  Task<ChatResponseDto?> GetExistingChatAsync(string rutaId, string usuario1Id, string usuario2Id);

  Task<bool> DeleteChatAsync(Guid chatId);
}

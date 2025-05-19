using Entity;
using System.Threading.Tasks;
using System;
public interface IChatRepository
{
    Task<bool> CreateChatAsync(ChatEntity chat);

    Task<ChatResponseDto?> GetExistingChatAsync(string rutaId, string usuario1Id, string usuario2Id);

    Task<bool> DeleteChatAsync(Guid chatId);
}
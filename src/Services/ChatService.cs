using System;
using System.Threading.Tasks;
using Dto.Chat;
using Entity.Chat;
using Repositories.Interface;
using System.Collections.Generic;
using Services.Interface;
namespace Services;

public class ChatService(IChatRepository chatRepository, IUserRepository userRepository) : IChatService
{
  private readonly IChatRepository _chatRepository = chatRepository;

  private readonly IUserRepository _userRepository = userRepository;

  public async Task<Guid?> CreateChatAsync(ChatRequestDto request)
  {
    if (request == null) throw new ArgumentNullException(nameof(request));

    var existing = await _chatRepository.GetExistingChatAsync(
        request.RutaId.ToString(),
        request.HostId.ToString(),
        request.JoinerId.ToString());

    if (existing != null)
        return existing.Id;

    bool isBlocked = await _userRepository.IsUserBlockedAsync(request.HostId, request.JoinerId)
                  || await _userRepository.IsUserBlockedAsync(request.JoinerId, request.HostId);

    if (isBlocked)
        throw new UnauthorizedAccessException("Uno de los usuarios ha bloqueado al otro.");

    var chat = new ChatEntity
    {
        ChatId    = Guid.NewGuid(),
        RouteId   = request.RutaId,
        User1Id   = request.HostId,
        User2Id   = request.JoinerId
    };

    bool isDone = await _chatRepository.CreateChatAsync(chat);
    return isDone ? chat.ChatId : null;
  }

  public async Task<bool> DeleteChatAsync(Guid chatId)
  {
    return await this._chatRepository.DeleteChatAsync(chatId).ConfigureAwait(false);
  }
  
  public Task<List<ChatResponseDto>> GetChatsForUserAsync(Guid userId)
  {
    if (userId == Guid.Empty)
    {
      throw new ArgumentException("El ID de usuario no puede ser vacío.", nameof(userId));
    }
    return this._chatRepository.GetChatsForUserAsync(userId);
  }
}


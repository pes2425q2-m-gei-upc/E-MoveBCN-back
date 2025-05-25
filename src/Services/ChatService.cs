using System;
using System.Threading.Tasks;
using Dto.Chat;
using Entity.Chat;
using Repositories.Interface;
using Services.Interface;
namespace Services;
public class ChatService(IChatRepository chatRepository, IUserRepository userRepository) : IChatService
{
  private readonly IChatRepository _chatRepository = chatRepository;

  private readonly IUserRepository _userRepository = userRepository;

  public async Task<string> CreateChatAsync(ChatRequestDto request)
  {
    if (request == null)
    {
      return "Request cannot be null.";
    }

    var existingChat = await this._chatRepository.GetExistingChatAsync(
        request.RutaId.ToString(), request.User1Id.ToString(), request.User2Id.ToString()).ConfigureAwait(false);

    if (existingChat != null)
    {
      return "Chat already exists for these users and route.";
    }
    var isBlocked = await this._userRepository.IsUserBlockedAsync(request.User1Id, request.User2Id).ConfigureAwait(false)
          || await this._userRepository.IsUserBlockedAsync(request.User2Id, request.User1Id).ConfigureAwait(false);

    if (isBlocked)
      return "Cannot create chat: one of the users is blocked by the other.";

    var chatEntity = new ChatEntity
    {
      ChatId = Guid.NewGuid(),
      RouteId = request.RutaId,
      User1Id = request.User1Id,
      User2Id = request.User2Id
    };
    var isDone = await this._chatRepository.CreateChatAsync(chatEntity).ConfigureAwait(false);
    if(isDone)
    {
      return "Chat created successfully.";
    }
    else
    {
      return "Couldnt create chat.";
    }
  }

  public async Task<bool> DeleteChatAsync(Guid chatId)
  {
    return await this._chatRepository.DeleteChatAsync(chatId).ConfigureAwait(false);
  }
}


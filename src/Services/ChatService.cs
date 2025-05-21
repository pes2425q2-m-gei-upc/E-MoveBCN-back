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

  public async Task<bool> CreateChatAsync(ChatRequestDto request)
  {
    if (request == null)
      throw new ArgumentNullException(nameof(request));

    var existingChat = await this._chatRepository.GetExistingChatAsync(
        request.RutaId.ToString(), request.User1Id.ToString(), request.User2Id.ToString()).ConfigureAwait(false);

    if (existingChat != null)
    {
      throw new Exception("Ya existe un chat entre estos usuarios para esta ruta.");
    }
    var isBlocked = await this._userRepository.IsUserBlockedAsync(request.User1Id, request.User2Id).ConfigureAwait(false)
          || await this._userRepository.IsUserBlockedAsync(request.User2Id, request.User1Id).ConfigureAwait(false);

    if (isBlocked)
      throw new Exception("No se puede crear un chat porque uno de los usuarios bloqueó al otro.");

    var chatEntity = new ChatEntity
    {
      ChatId = Guid.NewGuid(),
      RouteId = request.RutaId,
      User1Id = request.User1Id,
      User2Id = request.User2Id
    };
    return await this._chatRepository.CreateChatAsync(chatEntity).ConfigureAwait(false);
  }

  public async Task<bool> DeleteChatAsync(Guid chatId)
  {
    return await this._chatRepository.DeleteChatAsync(chatId).ConfigureAwait(false);
  }
}


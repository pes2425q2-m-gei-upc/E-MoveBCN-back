using System;
using System.Threading.Tasks;
using Dto;
using Entity;
using Repositories.Interface;
using Services.Interface;

namespace Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        
        private readonly IUserRepository _userRepository;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }

        public async Task<bool> CreateChatAsync(ChatRequestDto request)
        {
    
            var existingChat = await _chatRepository.GetExistingChatAsync(
                request.RutaId.ToString(), request.User1Id.ToString(), request.User2Id.ToString()).ConfigureAwait(false);

            if (existingChat != null)
            {
                throw new Exception("Ya existe un chat entre estos usuarios para esta ruta.");
            }
            var isBlocked =  await _userRepository.IsUserBlockedAsync(request.User1Id, request.User2Id).ConfigureAwait(false)
                  || await _userRepository.IsUserBlockedAsync(request.User2Id, request.User1Id).ConfigureAwait(false);

            if (isBlocked)
                throw new Exception("No se puede crear un chat porque uno de los usuarios bloqueó al otro.");

            var chatEntity = new ChatEntity
            {
                ChatId = Guid.NewGuid(),
                RouteId = request.RutaId,
                User1Id = request.User1Id,
                User2Id = request.User2Id
            };
            return await _chatRepository.CreateChatAsync(chatEntity).ConfigureAwait(false);
        }

        public async Task<bool> DeleteChatAsync(Guid chatId)
        {
            return await _chatRepository.DeleteChatAsync(chatId).ConfigureAwait(false);
        }



    }
}

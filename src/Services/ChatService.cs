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

        private readonly IRouteRepository _routeRepository;
        public ChatService(IChatRepository chatRepository)
        {
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

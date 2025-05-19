using System;
using System.Linq;
using System.Threading.Tasks;
using Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApiDbContext _dbcontext;
        private readonly IMapper _mapper;

        public ChatRepository(ApiDbContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateChatAsync(ChatEntity chat)
        {
            _dbcontext.Chats.Add(chat);
            return await _dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public async Task<ChatResponseDto?> GetExistingChatAsync(string rutaId, string usuario1Id, string usuario2Id)
        {
            var rutaGuid = Guid.Parse(rutaId);
            var user1Guid = Guid.Parse(usuario1Id);
            var user2Guid = Guid.Parse(usuario2Id);

            var chat = await _dbcontext.Chats.FirstOrDefaultAsync(c =>
                c.RouteId == rutaGuid &&
                ((c.User1Id == user1Guid && c.User2Id == user2Guid) ||
                (c.User1Id == user2Guid && c.User2Id == user1Guid))
            ).ConfigureAwait(false);

            return _mapper.Map<ChatResponseDto>(chat);
        }

        public async Task<bool> DeleteChatAsync(Guid chatId)
        {
            var messages = _dbcontext.Messages.Where(m => m.ChatId == chatId);
            _dbcontext.Messages.RemoveRange(messages); // Borra los mensajes primero
            
            var chat = await _dbcontext.Chats.FindAsync(chatId).ConfigureAwait(false);
            if (chat == null)
                return false;

            _dbcontext.Chats.Remove(chat);
            return await _dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using Dto;

namespace Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApiDbContext _dbcontext;
        private readonly IMapper _mapper;

        public MessageRepository(ApiDbContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<MessageDto?> CreateMessageAsync(MessageEntity message)
        {
            _dbcontext.Messages.Add(message);
            var saved = await _dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;

            return saved ? _mapper.Map<MessageDto>(message) : null;
        }

        public async Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId)
        {
            var messages = await _dbcontext.Messages
                .Where(m => m.ChatId == chatId)
                .OrderBy(m => m.CreatedAt)
                .ToListAsync()
                .ConfigureAwait(false);

            return _mapper.Map<List<MessageDto>>(messages);
        }
    }
}

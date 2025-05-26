#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Chat;
using Entity;
using Entity.Chat;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
namespace Repositories;

public class MessageRepository(ApiDbContext dbcontext, IMapper mapper) : IMessageRepository
{
  private readonly ApiDbContext _dbcontext = dbcontext;
  private readonly IMapper _mapper = mapper;

  public async Task<MessageDto?> CreateMessageAsync(MessageEntity message)
  {
    this._dbcontext.Messages.Add(message);
    var saved = await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;

    return saved ? this._mapper.Map<MessageDto>(message) : null;
  }
  public async Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId)
  {
    var messages = await this._dbcontext.Messages
        .Where(m => m.ChatId == chatId)
        .OrderBy(m => m.CreatedAt)
        .ToListAsync()
        .ConfigureAwait(false);

    return this._mapper.Map<List<MessageDto>>(messages);
  }

  public async Task<List<MessageDto>> GetMessagesBetweenAsync(Guid chatId, DateTime from, DateTime to)
  {
    var messages = await _dbcontext.Messages
      .Where(m => m.ChatId == chatId && m.CreatedAt >= from && m.CreatedAt <= to)
      .OrderBy(m => m.CreatedAt)
      .ToListAsync();

    return _mapper.Map<List<MessageDto>>(messages);
  }

  public async Task<List<MessageDto>> GetLastMessagesAsync(Guid chatId, int count)
  {
    var messages = await _dbcontext.Messages
      .Where(m => m.ChatId == chatId)
      .OrderByDescending(m => m.CreatedAt)
      .Take(count)
      .ToListAsync();

    return _mapper.Map<List<MessageDto>>(messages);
  }
}


#nullable enable
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using Dto.Chat;
using Entity;
using Entity.Chat;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories;

public class ChatRepository(ApiDbContext context, IMapper mapper) : IChatRepository
{
  private readonly ApiDbContext _dbcontext = context;
  private readonly IMapper _mapper = mapper;

  public async Task<bool> CreateChatAsync(ChatEntity chat)
  {
    this._dbcontext.Chats.Add(chat);
    return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

  public async Task<ChatResponseDto?> GetExistingChatAsync(string rutaId, string usuario1Id, string usuario2Id)
  {
    var rutaGuid = Guid.Parse(rutaId, System.Globalization.CultureInfo.InvariantCulture);
    var user1Guid = Guid.Parse(usuario1Id, System.Globalization.CultureInfo.InvariantCulture);
    var user2Guid = Guid.Parse(usuario2Id, System.Globalization.CultureInfo.InvariantCulture);

    var chat = await this._dbcontext.Chats.FirstOrDefaultAsync(c =>
        c.RouteId == rutaGuid &&
        ((c.User1Id == user1Guid && c.User2Id == user2Guid) ||
        (c.User1Id == user2Guid && c.User2Id == user1Guid))
    ).ConfigureAwait(false);

    return chat == null ? null : this._mapper.Map<ChatResponseDto>(chat);
  }

  public async Task<bool> DeleteChatAsync(Guid chatId)
  {
    var messages = this._dbcontext.Messages.Where(m => m.ChatId == chatId);
    this._dbcontext.Messages.RemoveRange(messages); // Borra los mensajes primero

    var chat = await this._dbcontext.Chats.FindAsync(chatId).ConfigureAwait(false);
    if (chat == null)
      return false;

    this._dbcontext.Chats.Remove(chat);
    return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

  public async Task<List<ChatResponseDto>> GetChatsForUserAsync(Guid userId)
  {
    var chats = await _dbcontext.Chats
      .Where(c => c.User1Id == userId || c.User2Id == userId)
      .ToListAsync();

    return _mapper.Map<List<ChatResponseDto>>(chats);
  }
}



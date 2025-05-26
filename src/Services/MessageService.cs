using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.Chat;
using Entity.Chat;
using Repositories.Interface;
using Services.Interface;
namespace Services;

public class MessageService(IMessageRepository messageRepository) : IMessageService
{
  private readonly IMessageRepository _messageRepository = messageRepository;
  private readonly IChatRepository _chatRepository;

  public async Task<MessageDto> SendMessageAsync(SendMessageDto dto)
  {
    if (dto == null)
    {
      throw new ArgumentNullException(nameof(dto));
    }

    var message = new MessageEntity
    {
      MessageId = Guid.NewGuid(),
      ChatId = dto.ChatId,
      UserId = dto.UserId,
      Message = dto.MessageText,
      CreatedAt = DateTime.UtcNow
    };

    var created = await this._messageRepository.CreateMessageAsync(message).ConfigureAwait(false);

    return new MessageDto
    {
      MessageId = created.MessageId,
      UserId = created.UserId,
      MessageText = created.MessageText,
      CreatedAt = created.CreatedAt
    };
  }

  public async Task<List<MessageDto>> GetMessagesByChatIdAsync(Guid chatId)
  {
    var messages = await this._messageRepository.GetMessagesByChatIdAsync(chatId).ConfigureAwait(false);

    return messages.Select(m => new MessageDto
    {
      MessageId = m.MessageId,
      UserId = m.UserId,
      MessageText = m.MessageText,
      CreatedAt = m.CreatedAt
    }).ToList();
  }

  public async Task<List<MessageDto>> GetMessagesBetweenAsync(Guid chatId, DateTime from, DateTime to)
  {
    if (from >= to)
    {
      throw new ArgumentException("La fecha de inicio debe ser anterior a la fecha de fin.");
    }
    var messages = await this._messageRepository.GetMessagesBetweenAsync(chatId, from, to).ConfigureAwait(false);

    return messages.Select(m => new MessageDto
    {
      MessageId = m.MessageId,
      UserId = m.UserId,
      MessageText = m.MessageText,
      CreatedAt = m.CreatedAt
    }).ToList();
  }

  public async Task<List<MessageDto>> GetLastMessagesAsync(Guid chatId, int count)
  {
    if (count <= 0)
    {
      throw new ArgumentException("El número de mensajes debe ser mayor que cero.");
    }

    var messages = await this._messageRepository.GetLastMessagesAsync(chatId, count).ConfigureAwait(false);

    return messages.Select(m => new MessageDto
    {
      MessageId = m.MessageId,
      UserId = m.UserId,
      MessageText = m.MessageText,
      CreatedAt = m.CreatedAt
    }).ToList();
  }
}

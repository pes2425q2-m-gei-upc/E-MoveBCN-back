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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto;
using Entity;
using Repositories.Interface;

public class MessageService : IMessageService
{
    private readonly IMessageRepository _messageRepository;

    public MessageService(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<MessageDto> SendMessageAsync(SendMessageDto dto)
    {
        var message = new MessageEntity
        {
            MessageId = Guid.NewGuid(),
            ChatId = dto.ChatId,
            UserId = dto.UserId,
            Message = dto.MessageText,
            CreatedAt = DateTime.UtcNow
        };

        var created = await _messageRepository.CreateMessageAsync(message);

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
        var messages = await _messageRepository.GetMessagesByChatIdAsync(chatId);

        return messages.Select(m => new MessageDto
        {
            MessageId = m.MessageId,
            UserId = m.UserId,
            MessageText = m.MessageText,
            CreatedAt = m.CreatedAt
        }).ToList();
    }
}

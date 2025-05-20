using Dto;
using System;
using System.Threading.Tasks;
public interface IChatService
{
    Task<bool> CreateChatAsync(ChatRequestDto request);
    Task<bool> DeleteChatAsync(Guid chatId);
}
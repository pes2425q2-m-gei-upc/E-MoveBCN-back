using System;
namespace Dto.Chat;
public class ChatResponseDto
{
  public Guid Id { get; set; }
  public Guid RutaId { get; set; }
  public Guid Usuario1Id { get; set; }
  public Guid Usuario2Id { get; set; }
  public DateTime CreatedAt { get; set; }
}

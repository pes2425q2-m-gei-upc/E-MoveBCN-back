using System;
namespace Dto.Chat;
public class MessageDto
{
  public Guid MessageId { get; set; }
  public Guid UserId { get; set; }
  public string MessageText { get; set; } = null!;
  public DateTime CreatedAt { get; set; }
}

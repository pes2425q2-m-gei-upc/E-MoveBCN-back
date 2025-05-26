using System;
namespace Dto.Chat;
public class ChatResponseDto
{
  public Guid Id { get; set; }
  public Guid RutaId { get; set; }
  public Guid HostId { get; set; }
  public Guid JoinerId { get; set; }
}

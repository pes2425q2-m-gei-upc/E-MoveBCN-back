using System;
namespace Dto.Chat;
public class BlockRequestDto
{
  public Guid BlockerId { get; set; }
  public Guid BlockedId { get; set; }
}

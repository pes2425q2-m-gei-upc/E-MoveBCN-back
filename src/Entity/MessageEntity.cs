using System;
using System.Collections.Generic;
namespace Entity;

public class MessageEntity
{
  public required Guid MessageId { get; set;}
  public required Guid ChatId { get; set; }
  public required Guid UserId{ get; set; }
  public required string Message { get; set; }
  public required DateTime CreatedAt { get; set; }

   public ChatEntity ChatIdNavigation { get; set; }

   public UserEntity UserIdNavigation { get; set; }
}
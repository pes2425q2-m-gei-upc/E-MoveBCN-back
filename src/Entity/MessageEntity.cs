using System;
using System.Collections.Generic;
namespace Entity;

public class MessageEntity
{
  public Guid MessageId { get; set;}
  public Guid ChatId { get; set; }
  public Guid UserId{ get; set; }
  public string Message { get; set; }
  public DateTime CreatedAt { get; set; }

   public ChatEntity ChatIdNavigation { get; set; }

   public UserEntity UserIdNavigation { get; set; }
}
﻿using System;
namespace Dto.Chat;
public class SendMessageDto
{
  public Guid ChatId { get; set; }
  public Guid UserId { get; set; }
  public string MessageText { get; set; } = null!;
}

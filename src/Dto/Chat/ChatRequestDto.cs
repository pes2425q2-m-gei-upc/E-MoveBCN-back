﻿using System;
namespace Dto.Chat;
public class ChatRequestDto
{
  public Guid HostId { get; set; }
  public Guid JoinerId { get; set; }
  public Guid RutaId { get; set; }
}

﻿using System;
using Entity.User;
namespace Entity.Chat;
public class UserBlockEntity
{
  public required Guid BlockerId { get; set; }
  public required Guid BlockedId { get; set; }

  public UserEntity Blocker { get; set; }
  public UserEntity Blocked { get; set; }
}

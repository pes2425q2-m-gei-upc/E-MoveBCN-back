﻿using System;
namespace Entity.User;
public class UserEntity
{
  public Guid UserId { get; set; }
  public string Username { get; set; }
  public string Email { get; set; }
  public string PasswordHash { get; set; }
}

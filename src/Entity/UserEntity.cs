using System;

namespace Entity;

public class UserEntity
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Idioma { get; set; }
}
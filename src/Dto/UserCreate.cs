namespace Dto;

public class UserCreate {
    public required string Name {get; set;}
    public required string Email {get; set;}
    public required string PasswordHash {get; set;}
    public required string Idioma {get; set;}
}
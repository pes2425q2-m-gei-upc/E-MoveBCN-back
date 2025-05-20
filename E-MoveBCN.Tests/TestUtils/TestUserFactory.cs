using Dto;

namespace TestUtils;

public static class TestUserFactory
{
    public static UserDto CreateValidUserDto(
        string? email = null,
        string? passwordHash = null,
        string? userId = null,
        string? username = null)
    {
        return new UserDto
        {
            UserId = userId ?? Guid.NewGuid().ToString(),
            Username = username ?? "testuser",
            Email = email ?? "test@example.com",
            PasswordHash = passwordHash ?? "hashed-password"
        };
    }

    public static UserCreate CreateValidUserCreate(
        string? email = null,
        string? passwordHash = null,
        string? username = null)
    {
        return new UserCreate
        {
            Username = username ?? "newuser",
            Email = email ?? "new@example.com",
            PasswordHash = passwordHash ?? "hashed-password"
        };
    }

    public static UserCredentials CreateUserCredentials(string email = "test@example.com", string password = "password")
    {
        return new UserCredentials
        {
            UserEmail = email,
            Password = password
        };
    }

    public static LoginGoogleDto CreateGoogleDto(string email = "test@example.com", string username = "testuser")
    {
        return new LoginGoogleDto
        {
            Email = email,
            Username = username
        };
    }
}

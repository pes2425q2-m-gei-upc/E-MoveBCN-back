namespace Dto.User;

public class UserDto
{

  public required string UserId { get; set; }
  public required string Username { get; set; }
  public required string Email { get; set; }
  public required string PasswordHash { get; set; }
}

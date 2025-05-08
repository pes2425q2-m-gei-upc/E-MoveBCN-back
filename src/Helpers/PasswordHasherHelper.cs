using Dto;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Helpers;

public class PasswordHasherHelper
{
  private const int _iterations = 210_000;
  private readonly PasswordHasher<UserDto> _passwordHasher;
  public PasswordHasherHelper()
  {
    var hasherOptions = new PasswordHasherOptions { IterationCount = _iterations };
    var options = Options.Create(hasherOptions);
    _passwordHasher = new PasswordHasher<UserDto>(options);
  }

  public string HashPassword(string password)
  {
    return _passwordHasher.HashPassword(null, password);
  }

  public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
  {
    return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
  }
}

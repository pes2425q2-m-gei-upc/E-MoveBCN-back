using Dto.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Helpers;

public class PasswordHasherHelper
{
  private const int Iterations = 210_000;
  private readonly PasswordHasher<UserDto> _passwordHasher;
  public PasswordHasherHelper()
  {
    var hasherOptions = new PasswordHasherOptions { IterationCount = Iterations };
    var options = Options.Create(hasherOptions);
    this._passwordHasher = new PasswordHasher<UserDto>(options);
  }

  public string HashPassword(string password)
  {
    return this._passwordHasher.HashPassword(null, password);
  }

  public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
  {
    return this._passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
  }
}

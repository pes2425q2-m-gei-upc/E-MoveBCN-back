using System;
using System.Collections.Generic;
using System.Security.Claims;
using Constants;
using Dto.User;
using Helpers.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace Helpers;
public class AuthenticationHelper : IAuthenticationHelper
{
  public AuthenticationHelper()
  {

  }

  public (ClaimsIdentity ClaimsIdentity, AuthenticationProperties AuthenticationProperties) AuthenticationClaims(UserDto user)
  {
    if (user == null)
    {
      throw new ArgumentNullException(nameof(user));
    }
    var claims = new List<Claim>
        {
            new(ApiClaimTypes.Name, user.Username),
            new(ApiClaimTypes.Email, user.Email),
            new(ApiClaimTypes.UserId, user.UserId.ToString())
        };
    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var expirationDate = DateTimeOffset.UtcNow.AddDays(1);
    var authenticationProperties = new AuthenticationProperties
    {
      IsPersistent = true,
      ExpiresUtc = expirationDate
    };
    return (claimsIdentity, authenticationProperties);
  }
}

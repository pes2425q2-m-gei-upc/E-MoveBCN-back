using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Constants;
using Dto;
using Helpers.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Repositories.Interface;

namespace Helpers;

public class AuthenticationHelper: IAuthenticationHelper
{
    public AuthenticationHelper()
    {

    }

    public (ClaimsIdentity ClaimsIdentity, AuthenticationProperties AuthenticationProperties) AuthenticationClaims(UserDto user)
    {
        var claims = new List<Claim>
        {
            new Claim(ApiClaimTypes.Name, user.Name),
            new Claim(ApiClaimTypes.Email, user.Email),
            new Claim(ApiClaimTypes.UserId, user.UserId.ToString()),
            new Claim(ApiClaimTypes.Idioma, user.Idioma)
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
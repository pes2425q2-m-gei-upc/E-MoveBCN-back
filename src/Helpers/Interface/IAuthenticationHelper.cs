using System.Security.Claims;
using Dto.User;
using Microsoft.AspNetCore.Authentication;

namespace Helpers.Interface;

public interface IAuthenticationHelper
{
  (ClaimsIdentity ClaimsIdentity, AuthenticationProperties AuthenticationProperties) AuthenticationClaims(UserDto user);
}

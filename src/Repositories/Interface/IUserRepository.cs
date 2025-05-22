#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.User;
namespace Repositories.Interface;
public interface IUserRepository
{
  public List<UserDto> GetAllUsers();
  public bool CreateUser(UserCreate user);
  public Task<bool> DeleteUser(string userId);
  public Task<bool> ModifyUser(UserDto userModify);
  public Task<UserDto?> GetUserById(string userId);
  Task<UserDto?> GetUserByEmailAsync(string email);
  Task<bool> CreateGoogleUserAsync(string name, string email);

  Task<bool> IsUserBlockedAsync(Guid blockerId, Guid blockedId);
  Task<bool> BlockUserAsync(Guid blockerId, Guid blockedId);
  Task<bool> UnblockUserAsync(Guid blockerId, Guid blockedId);
}

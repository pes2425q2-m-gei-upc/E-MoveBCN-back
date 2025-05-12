using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
using Entity;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories;

public class UserRepository : IUserRepository
{
  private readonly ApiDbContext _Dbcontext;
  private readonly IMapper _mapper;

  public UserRepository(ApiDbContext context, IMapper mapper)
  {
    _Dbcontext = context;
    _mapper = mapper;
  }

  public List<UserDto> GetAllUsers()
  {
    var entities = _Dbcontext.Users
        .Select(u => new UserDto
        {
          UserId = u.UserId.ToString(),
          Username = u.Username,
          Email = u.Email,
          PasswordHash = u.PasswordHash,
        })
        .ToList();
    return _mapper.Map<List<UserDto>>(entities);
  }

  public bool CreateUser(UserCreate userDto)
  {
    try
    {
      var passwordHasher = new PasswordHasherHelper();
      var password = passwordHasher.HashPassword(userDto.PasswordHash);

      var user = new UserEntity
      {
        UserId = Guid.NewGuid(),
        Username = userDto.Username,
        Email = userDto.Email,
        PasswordHash = password,
      };

      _Dbcontext.Users.Add(user);
      return _Dbcontext.SaveChanges() > 0;
    }
    catch (Exception)
    {
      return false;
    }
  }
  public async Task<bool> DeleteUser(string userId)
  {
    if (!Guid.TryParse(userId, out Guid parsedUserId))
      return false;

    var user = await _Dbcontext.Users
        .FirstOrDefaultAsync(u => u.UserId == parsedUserId).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }

    await _Dbcontext.SavedUbications
        .Where(u => u.UserEmail == user.Email)
        .ExecuteDeleteAsync().ConfigureAwait(false);

    int deletedRows = await _Dbcontext.Users
        .Where(u => u.UserId == parsedUserId)
        .ExecuteDeleteAsync().ConfigureAwait(false);

    return deletedRows > 0;
  }

  public async Task<bool> ModifyUser(UserDto userModify)
  {
    var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userModify.UserId)).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    if (userModify.PasswordHash != null && !string.IsNullOrEmpty(userModify.PasswordHash))
    {
      var passwordHasher = new PasswordHasherHelper();
      var password = passwordHasher.HashPassword(userModify.PasswordHash);
      user.PasswordHash = password;
    }

    user.Username = userModify.Username;

    _Dbcontext.Users.Update(user);
    return await _Dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

  public async Task<UserDto?> GetUserByEmailAsync(string email)
  {
    var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);
    return _mapper.Map<UserDto>(user);
  }
  public async Task<UserDto?> GetUserById(string userId)
  {
    var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userId)).ConfigureAwait(false);
    return _mapper.Map<UserDto>(user);
  }

  public async Task<bool> CreateGoogleUserAsync(string name, string email)
  {
    try
    {
      var user = new UserEntity
      {
        UserId = Guid.NewGuid(),
        Username = name,
        Email = email,
        PasswordHash = "", // No password for google users
      };

      _Dbcontext.Users.Add(user);
      return await _Dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
    }
    catch
    {
      return false;
    }
  }
}

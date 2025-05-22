#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.User;
using Entity;
using Entity.Chat;
using Entity.User;
using Helpers;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories;

public class UserRepository(ApiDbContext context, IMapper mapper) : IUserRepository
{
  private readonly ApiDbContext _dbcontext = context;
  private readonly IMapper _mapper = mapper;

  public List<UserDto> GetAllUsers()
  {
    var entities = this._dbcontext.Users
        .Select(u => new UserDto
        {
          UserId = u.UserId.ToString(),
          Username = u.Username,
          Email = u.Email,
          PasswordHash = u.PasswordHash,
        })
        .ToList();
    return this._mapper.Map<List<UserDto>>(entities);
  }

  public bool CreateUser(UserCreate userDto)
  {
    if (userDto == null)
    {
      throw new ArgumentNullException(nameof(userDto));
    }
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

      this._dbcontext.Users.Add(user);
      return this._dbcontext.SaveChanges() > 0;
    }
    catch (DbUpdateException)
    {
      // Handle database update exceptions specifically
      return false;
    }
    catch (ArgumentException)
    {
      // Handle argument exceptions specifically
      return false;
    }
    catch
    {
      // Rethrow any other exceptions
      throw;
    }
  }
  public async Task<bool> DeleteUser(string userId)
  {
    if (!Guid.TryParse(userId, out Guid parsedUserId))
      return false;

    var user = await this._dbcontext.Users
        .FirstOrDefaultAsync(u => u.UserId == parsedUserId).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }

    await this._dbcontext.SavedUbications
        .Where(u => u.UserEmail == user.Email)
        .ExecuteDeleteAsync().ConfigureAwait(false);

    int deletedRows = await this._dbcontext.Users
        .Where(u => u.UserId == parsedUserId)
        .ExecuteDeleteAsync().ConfigureAwait(false);

    return deletedRows > 0;
  }

  public async Task<bool> ModifyUser(UserDto userModify)
  {
    if (userModify == null)
    {
      throw new ArgumentNullException(nameof(userModify));
    }

    var user = await this._dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userModify.UserId, System.Globalization.CultureInfo.InvariantCulture)).ConfigureAwait(false);
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

    this._dbcontext.Users.Update(user);
    return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

  public async Task<UserDto?> GetUserByEmailAsync(string email)
  {
    var user = await this._dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email).ConfigureAwait(false);
    return this._mapper.Map<UserDto>(user);
  }
  public async Task<UserDto?> GetUserById(string userId)
  {
    var user = await this._dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userId, System.Globalization.CultureInfo.InvariantCulture)).ConfigureAwait(false);
    return this._mapper.Map<UserDto>(user);
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

      this._dbcontext.Users.Add(user);
      return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
    }
    catch (DbUpdateException)
    {
      // Handle database update exceptions specifically
      return false;
    }
    catch (ArgumentException)
    {
      // Handle argument exceptions specifically
      return false;
    }
    catch
    {
      // Rethrow any other exceptions
      throw;
    }
  }

  public async Task<bool> IsUserBlockedAsync(Guid blockerId, Guid blockedId)
  {
    return await this._dbcontext.UserBlocks.AnyAsync(ub => ub.BlockerId == blockerId && ub.BlockedId == blockedId).ConfigureAwait(false);
  }

  public async Task<bool> BlockUserAsync(Guid blockerId, Guid blockedId)
  {
    if (await IsUserBlockedAsync(blockerId, blockedId).ConfigureAwait(false)) return false;

    this._dbcontext.UserBlocks.Add(new UserBlockEntity
    {
      BlockerId = blockerId,
      BlockedId = blockedId
    });

    return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

  public async Task<bool> UnblockUserAsync(Guid blockerId, Guid blockedId)
  {
    var block = await this._dbcontext.UserBlocks
        .FirstOrDefaultAsync(ub => ub.BlockerId == blockerId && ub.BlockedId == blockedId).ConfigureAwait(false);

    if (block == null) return false;

    this._dbcontext.UserBlocks.Remove(block);
    return await this._dbcontext.SaveChangesAsync().ConfigureAwait(false) > 0;
  }

}

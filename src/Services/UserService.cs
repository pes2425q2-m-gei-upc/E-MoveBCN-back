using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto.Chat;
using Dto.User;
using Helpers;
using Microsoft.AspNetCore.Identity;
using Repositories.Interface;
using Services.Interface;
namespace Services;

public class UserService(IUserRepository userRepository) : IUserService
{
  private readonly IUserRepository _userRepository = userRepository;

  public List<UserDto> GetAllUsers()
  {
    return this._userRepository.GetAllUsers();
  }
  public bool CreateUser(UserCreate user)
  {
    return this._userRepository.CreateUser(user);
  }
  public async Task<UserDto> Authenticate(UserCredentials userCredentials)
  {
    if (userCredentials == null)
    {
      throw new ArgumentNullException(nameof(userCredentials));
    }
    var user = await this._userRepository.GetUserByEmailAsync(userCredentials.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return null;
    }
    var passwordHasher = new PasswordHasherHelper();
    var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, userCredentials.Password);
    if (verificationResult == PasswordVerificationResult.Failed)
    {
      return null;
    }
    return user;
  }

  public async Task<bool> DeleteUser(UserCredentials userCredentials)
  {
    if (userCredentials == null)
    {
      throw new ArgumentNullException(nameof(userCredentials));
    }
    var user = await this._userRepository.GetUserByEmailAsync(userCredentials.UserEmail).ConfigureAwait(false);
    if (user == null)
    {
      return false;
    }
    var passwordHasher = new PasswordHasherHelper();
    var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, userCredentials.Password);
    if (verificationResult == PasswordVerificationResult.Failed && !string.IsNullOrEmpty(user.PasswordHash) && !user.Email.Contains("@gmail.com"))
    {
      return false;
    }
    return await this._userRepository.DeleteUser(user.UserId).ConfigureAwait(false);
  }
  public async Task<bool> ModifyUser(UserDto userModify)
  {
    return await this._userRepository.ModifyUser(userModify).ConfigureAwait(false);
  }

  public async Task<UserDto> LoginWithGoogleAsync(LoginGoogleDto dto)
  {
    if (dto == null)
    {
      throw new ArgumentNullException(nameof(dto));
    }
    var existingUser = await this._userRepository.GetUserByEmailAsync(dto.Email).ConfigureAwait(false);
    if (existingUser != null)
    {
      return existingUser;
    }
    var created = await this._userRepository.CreateGoogleUserAsync(dto.Username, dto.Email).ConfigureAwait(false);

    if (!created)
    {
      throw new Exception("Error al crear usuario con Google");
    }


    var newUser = await this._userRepository.GetUserByEmailAsync(dto.Email).ConfigureAwait(false) ?? throw new Exception("Usuario creado pero no se puede obtener");
    return newUser;
  }

  public async Task<bool> BlockUserAsync(BlockRequestDto dto)
  {
    if (dto == null)
      throw new ArgumentNullException(nameof(dto));

    if (dto.BlockerId == dto.BlockedId)
      throw new ArgumentException("No puedes bloquearte a ti mismo.");

    if (await this._userRepository.IsUserBlockedAsync(dto.BlockerId, dto.BlockedId).ConfigureAwait(false))
      throw new ArgumentException("El usuario ya está bloqueado.");

    return await this._userRepository.BlockUserAsync(dto.BlockerId, dto.BlockedId).ConfigureAwait(false);
  }
  public async Task<bool> UnblockUserAsync(BlockRequestDto dto)
  {
    if (dto == null)
      throw new ArgumentNullException(nameof(dto));

    if (dto.BlockerId == dto.BlockedId)
      throw new ArgumentException("No puedes desbloquearte a ti mismo.");

    if (!await this._userRepository.IsUserBlockedAsync(dto.BlockerId, dto.BlockedId).ConfigureAwait(false))
      throw new ArgumentException("El usuario no está bloqueado.");

    return await this._userRepository.UnblockUserAsync(dto.BlockerId, dto.BlockedId).ConfigureAwait(false);
  }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dto;
using Helpers;
using Microsoft.AspNetCore.Identity;
using Repositories.Interface;
using Services.Interface;
namespace src.Services;

public class UserService : IUserService
{
  private readonly IUserRepository _userRepository;

  public UserService(IUserRepository userRepository)
  {
    _userRepository = userRepository;
  }

  public List<UserDto> GetAllUsers()
  {
    return _userRepository.GetAllUsers();
  }
  public bool CreateUser(UserCreate user)
  {
    return _userRepository.CreateUser(user);
  }
  public async Task<UserDto> Authenticate(UserCredentials userCredentials)
  {
    var user = await _userRepository.GetUserByEmailAsync(userCredentials.UserEmail).ConfigureAwait(false);
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
    var user = await _userRepository.GetUserByEmailAsync(userCredentials.UserEmail).ConfigureAwait(false);
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
    return await _userRepository.DeleteUser(user.UserId).ConfigureAwait(false);
  }
  public async Task<bool> ModifyUser(UserDto userModify)
  {
    return await _userRepository.ModifyUser(userModify).ConfigureAwait(false);
  }

  public async Task<UserDto> LoginWithGoogleAsync(LoginGoogleDto dto)
  {
    var existingUser = await _userRepository.GetUserByEmailAsync(dto.Email).ConfigureAwait(false);
    if (existingUser != null)
    {
      return existingUser;
    }
    var created = await _userRepository.CreateGoogleUserAsync(dto.Username, dto.Email).ConfigureAwait(false);

    if (!created)
    {
      throw new Exception("Error al crear usuario con Google");
    }


    var newUser = await _userRepository.GetUserByEmailAsync(dto.Email).ConfigureAwait(false);
    if (newUser == null)
    {
      throw new Exception("Usuario creado pero no se puede obtener");
    }

    return newUser;
  }

}

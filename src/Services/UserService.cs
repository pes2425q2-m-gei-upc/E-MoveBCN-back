using Services.Interface;
using Repositories.Interface;
using System.Collections.Generic;
using Dto;
using System;
using System.Threading.Tasks;
using Helpers;
using Microsoft.AspNetCore.Identity;
namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService (IUserRepository userRepository) {
        _userRepository = userRepository;
    }

    public List<UserDto> GetAllUsers() {
        return  _userRepository.GetAllUsers();
    }
    public bool CreateUser(UserCreate user) {
        return _userRepository.CreateUser(user);
    }
    public async Task<UserDto?> Authenticate(UserCredentials userCredentials) {
        var user = await _userRepository.GetUserByUsername(userCredentials.Username);
        if (user == null) {
            return null;
        }
        var passwordHasher = new PasswordHasherHelper();
        var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, userCredentials.Password);
        if (verificationResult == PasswordVerificationResult.Failed) {
            return null;
        }
        return user;
    }

    public async Task<bool> DeleteUser(UserCredentials userCredentials) {
        var user = await _userRepository.GetUserByUsername(userCredentials.Username);
        if (user == null) {
            return false;
        }
        var passwordHasher = new PasswordHasherHelper();
        var verificationResult = passwordHasher.VerifyHashedPassword(user.PasswordHash, userCredentials.Password);
        if (verificationResult == PasswordVerificationResult.Failed) {
            return false;
        }
        return await _userRepository.DeleteUser(user.UserId);
    }
    public async Task<bool> ModifyUser(UserDto userModify) {
        return await _userRepository.ModifyUser(userModify);
    }

    public async Task<UserDto> LoginWithGoogleAsync(string name, string email)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(email);

        if (existingUser == null)
        {
            var success = await _userRepository.CreateGoogleUserAsync(name, email);
            if (!success)
            {
                throw new Exception("Error creating Google user.");
            }

            existingUser = await _userRepository.GetUserByEmailAsync(email);
        }

        return existingUser!;
    }

}
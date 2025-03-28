using Repositories.Interface;
using Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System;
using Helpers;

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
                Name = u.Name,
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
                Name = userDto.Name,
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
    public async Task<UserDto> GetUserByUsername(string username)
    {
        var user = await _Dbcontext.Users
            .FirstOrDefaultAsync(u => u.Name == username);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteUser(string userId)
    {
        if (!Guid.TryParse(userId, out Guid parsedUserId))
            return false; 

        int deletedRows = await _Dbcontext.Users
            .Where(u => u.UserId == parsedUserId)
            .ExecuteDeleteAsync();

        return deletedRows > 0;
    }

    public async Task<bool> ModifyUser(UserDto userModify) 
    {
        var user = await _Dbcontext.Users.FirstOrDefaultAsync(u => u.UserId == Guid.Parse(userModify.UserId));
        if (user == null)
        {
            return false;
        }

        var passwordHasher = new PasswordHasherHelper();
        var password = passwordHasher.HashPassword(userModify.PasswordHash);

        user.Name = userModify.Name;
        user.Email = userModify.Email;
        user.PasswordHash = password;

        _Dbcontext.Users.Update(user);
        return await _Dbcontext.SaveChangesAsync() > 0;
    }
}
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
                Idioma = u.Idioma
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
                Idioma = userDto.Idioma
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
}
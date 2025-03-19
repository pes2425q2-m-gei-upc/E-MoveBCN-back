using Repositories.Interface;
using Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using System;

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
}
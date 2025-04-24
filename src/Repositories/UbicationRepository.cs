using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories;

public class UbicationRepository : IUbicationRepository
{
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;

    public UbicationRepository(ApiDbContext context, IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string userId)
    {
        var entities = await _context.SavedUbications
            .Where(u => u.UserId == Guid.Parse(userId))
            .ToListAsync();
        return _mapper.Map<List<SavedUbicationDto>>(entities);
    }
}
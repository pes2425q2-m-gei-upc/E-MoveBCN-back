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

    public async Task<List<SavedUbicationDto>> GetUbicationsByUserIdAsync(string username)
    {
        var entities = await _context.SavedUbications
            .Where(u => u.Username == username)
            .ToListAsync();
        return _mapper.Map<List<SavedUbicationDto>>(entities);
    }
    public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
    {
        var savedUbicationEntity = new SavedUbicationEntity
        {
            Username = savedUbication.Username,
            UbicationId = savedUbication.UbicationId,
            Latitude = savedUbication.Latitude,
            Longitude = savedUbication.Longitude,
            StationType = savedUbication.StationType,
        };
        await _context.SavedUbications.AddAsync(savedUbicationEntity);
        return await _context.SaveChangesAsync() > 0;
    }
}
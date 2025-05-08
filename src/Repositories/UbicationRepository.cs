using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
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
        .ToListAsync().ConfigureAwait(false);
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
    await _context.SavedUbications.AddAsync(savedUbicationEntity).ConfigureAwait(false);
    return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
  {
    var savedUbication = await _context.SavedUbications
        .FirstOrDefaultAsync(u => u.Username == ubicationDelete.Username && u.UbicationId == ubicationDelete.UbicationId && u.StationType == ubicationDelete.StationType).ConfigureAwait(false);
    if (savedUbication == null)
    {
      return false;
    }
    _context.SavedUbications.Remove(savedUbication);
    return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
  {
    var savedUbicationEntity = await _context.SavedUbications
        .FirstOrDefaultAsync(u => u.Username == savedUbication.Username && u.UbicationId == savedUbication.UbicationId).ConfigureAwait(false);
    if (savedUbicationEntity == null)
    {
      return false;
    }
    savedUbicationEntity.Valoration = savedUbication.Valoration;
    savedUbicationEntity.Comment = savedUbication.Comment;
    return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
}

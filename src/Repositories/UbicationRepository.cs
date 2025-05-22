using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Ubication;
using Entity;
using Entity.Ubication;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;

namespace Repositories;

public class UbicationRepository(ApiDbContext context, IMapper mapper) : IUbicationRepository
{
  private readonly ApiDbContext _context = context;
  private readonly IMapper _mapper = mapper;

  public async Task<List<SavedUbicationDto>> GetUbicationsByEmailAsync(string userEmail)
  {
    var entities = await this._context.SavedUbications
        .Where(u => u.UserEmail == userEmail)
        .ToListAsync().ConfigureAwait(false);
    return this._mapper.Map<List<SavedUbicationDto>>(entities);
  }
  public async Task<bool> SaveUbicationAsync(SavedUbicationDto savedUbication)
  {
    if (savedUbication == null)
    {
      throw new ArgumentNullException(nameof(savedUbication));
    }
    var savedUbicationEntity = new SavedUbicationEntity
    {
      UserEmail = savedUbication.UserEmail,
      UbicationId = savedUbication.UbicationId,
      Latitude = savedUbication.Latitude,
      Longitude = savedUbication.Longitude,
      StationType = savedUbication.StationType,
    };
    await this._context.SavedUbications.AddAsync(savedUbicationEntity).ConfigureAwait(false);
    return await this._context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> DeleteUbication(UbicationInfoDto ubicationDelete)
  {
    var savedUbication = await this._context.SavedUbications
        .FirstOrDefaultAsync(u => u.UserEmail == ubicationDelete.UserEmail && u.UbicationId == ubicationDelete.UbicationId && u.StationType == ubicationDelete.StationType).ConfigureAwait(false);
    if (savedUbication == null)
    {
      return false;
    }
    this._context.SavedUbications.Remove(savedUbication);
    return await this._context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
  public async Task<bool> UpdateUbication(UbicationInfoDto savedUbication)
  {
    if (savedUbication == null)
    {
      throw new ArgumentNullException(nameof(savedUbication));
    }

    var savedUbicationEntity = await this._context.SavedUbications
        .FirstOrDefaultAsync(u =>
            u.UserEmail == savedUbication.UserEmail &&
            u.UbicationId == savedUbication.UbicationId &&
            u.StationType == savedUbication.StationType
        ).ConfigureAwait(false);

    if (savedUbicationEntity == null)
    {
      return false;
    }

    savedUbicationEntity.Valoration = savedUbication.Valoration;
    savedUbicationEntity.Comment = savedUbication.Comment;

    return await this._context.SaveChangesAsync().ConfigureAwait(false) > 0;
  }
}

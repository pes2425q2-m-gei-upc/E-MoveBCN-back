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

public class StateBicingRepository : IStateBicingRepository
{
  private readonly ApiDbContext _dbContext;
  private readonly IMapper _mapper;

  public StateBicingRepository(ApiDbContext dbContext, IMapper mapper)
  {
    _dbContext = dbContext;
    _mapper = mapper;
  }

  public async Task BulkInsertAsync(
      List<StateBicingEntity> statebicingstations)
  {
    using (var transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false))
    {
      try
      {
        await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE state_bicing CASCADE").ConfigureAwait(false);

        await _dbContext.StateBicing.AddRangeAsync(statebicingstations).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync().ConfigureAwait(false);

        await transaction.CommitAsync().ConfigureAwait(false);
      }
      catch (Exception)
      {
        await transaction.RollbackAsync().ConfigureAwait(false);
        throw;
      }
    }
  }

  public async Task<List<StateBicingDto>> GetAllStateBicingStations()
  {
    var entities = await _dbContext.StateBicing
        .Select(s => new StateBicingEntity
        {
          BicingId = s.BicingId,
          NumBikesAvailable = s.NumBikesAvailable,
          NumBikesAvailableMechanical = s.NumBikesAvailableMechanical,
          NumBikesAvailableEbike = s.NumBikesAvailableEbike,
          NumDocksAvailable = s.NumDocksAvailable,
          LastReported = s.LastReported,
          Status = s.Status,
          BicingStationIdNavigation = s.BicingStationIdNavigation
        })
        .ToListAsync().ConfigureAwait(false);

    return _mapper.Map<List<StateBicingDto>>(entities);
  }

  public async Task<StateBicingDto> GetStateBicingById(int id)
  {
    var entity = await _dbContext.StateBicing
        .FirstOrDefaultAsync(s => s.BicingId == id).ConfigureAwait(false);

    if (entity == null)
    {
      return null;
    }

    return _mapper.Map<StateBicingDto>(entity);
  }

}

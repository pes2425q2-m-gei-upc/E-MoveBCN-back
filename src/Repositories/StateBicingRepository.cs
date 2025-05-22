using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dto.Bicing;
using Entity;
using Entity.Bicing;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
namespace Repositories;
public class StateBicingRepository(ApiDbContext dbContext, IMapper mapper) : IStateBicingRepository
{
  private readonly ApiDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task BulkInsertAsync(
      List<StateBicingEntity> statebicingstations)
  {
    using var transaction = await this._dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
    try
    {
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE state_bicing CASCADE").ConfigureAwait(false);

      await this._dbContext.StateBicing.AddRangeAsync(statebicingstations).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await transaction.CommitAsync().ConfigureAwait(false);
    }
    catch (Exception)
    {
      await transaction.RollbackAsync().ConfigureAwait(false);
      throw;
    }
  }

  public async Task<List<StateBicingDto>> GetAllStateBicingStations()
  {
    var entities = await this._dbContext.StateBicing
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

    return this._mapper.Map<List<StateBicingDto>>(entities);
  }

  public async Task<StateBicingDto> GetStateBicingById(int id)
  {
    var entity = await this._dbContext.StateBicing
        .FirstOrDefaultAsync(s => s.BicingId == id).ConfigureAwait(false);

    if (entity == null)
    {
      return null;
    }

    return this._mapper.Map<StateBicingDto>(entity);
  }

}

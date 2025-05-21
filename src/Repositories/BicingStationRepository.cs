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
public class BicingStationRepository(ApiDbContext dbContext, IMapper mapper) : IBicingStationRepository
{
  private readonly ApiDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task BulkInsertAsync(
      List<BicingStationEntity> bicingstations)
  {
    using var transaction = await this._dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
    try
    {
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bicing_station CASCADE").ConfigureAwait(false);

      await this._dbContext.BicingStations.AddRangeAsync(bicingstations).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await transaction.CommitAsync().ConfigureAwait(false);
    }
    catch (Exception)
    {
      await transaction.RollbackAsync().ConfigureAwait(false);
      throw;
    }
  }

  public async Task<List<BicingStationDto>> GetAllBicingStations()
  {
    var entities = await this._dbContext.BicingStations
        .Select(s => new BicingStationEntity
        {
          BicingId = s.BicingId,
          BicingName = s.BicingName,
          Latitude = s.Latitude,
          Longitude = s.Longitude,
          Altitude = s.Altitude,
          Address = s.Address,
          CrossStreet = s.CrossStreet,
          PostCode = s.PostCode,
          Capacity = s.Capacity,
          IsChargingStation = s.IsChargingStation
        })
        .ToListAsync().ConfigureAwait(false);

    return this._mapper.Map<List<BicingStationDto>>(entities);
  }
  public async Task<BicingStationDto> GetBicingStationDetails(int id)
  {
    var entity = await this._dbContext.BicingStations
        .FirstOrDefaultAsync(s => s.BicingId == id).ConfigureAwait(false);

    if (entity == null)
    {
      return null;
    }

    return this._mapper.Map<BicingStationDto>(entity);
  }
}

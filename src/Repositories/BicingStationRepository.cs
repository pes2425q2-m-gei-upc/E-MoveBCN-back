using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Dto;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interface;
namespace Repositories;

public class BicingStationRepository : IBicingStationRepository
{
  private readonly ApiDbContext _dbContext;
  private readonly IMapper _mapper;

  private readonly ILogger<BicingStationRepository> _logger;

  public BicingStationRepository(ApiDbContext dbContext, IMapper mapper, ILogger<BicingStationRepository> logger)
  {
    _dbContext = dbContext;
    _mapper = mapper;
    _logger = logger;
  }

  public async Task BulkInsertAsync(
      List<BicingStationEntity> bicingstations)
  {
    using (var transaction = await _dbContext.Database.BeginTransactionAsync().ConfigureAwait(false))
    {
      try
      {
        await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bicing_station CASCADE").ConfigureAwait(false);

        await _dbContext.BicingStations.AddRangeAsync(bicingstations).ConfigureAwait(false);
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

  public async Task<List<BicingStationDto>> GetAllBicingStations()
  {
    var entities = await _dbContext.BicingStations
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

    return _mapper.Map<List<BicingStationDto>>(entities);
  }
  public async Task<BicingStationDto> GetBicingStationDetails(int id)
  {
    var entity = await _dbContext.BicingStations
        .FirstOrDefaultAsync(s => s.BicingId == id).ConfigureAwait(false);

    if (entity == null)
    {
      return null;
    }

    return _mapper.Map<BicingStationDto>(entity);
  }
}

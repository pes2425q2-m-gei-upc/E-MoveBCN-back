using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using AutoMapper;
using System.Linq;
using Dto;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;
namespace Repositories
{
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
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE bicing_station CASCADE");

                    await _dbContext.BicingStations.AddRangeAsync(bicingstations);
                    await _dbContext.SaveChangesAsync(); 

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
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
                .ToListAsync();

            return _mapper.Map<List<BicingStationDto>>(entities);
        }
        public async Task<BicingStationDto> GetBicingStationDetails(int id)
        {
            var entity = await _dbContext.BicingStations
                .FirstOrDefaultAsync(s => s.BicingId == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<BicingStationDto>(entity);
        }
    }
}
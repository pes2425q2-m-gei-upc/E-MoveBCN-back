using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using AutoMapper;
using System.Linq;
using Dto;
namespace Repositories
{
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
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE state_bicing CASCADE");

                    await _dbContext.StateBicing.AddRangeAsync(statebicingstations);
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
                .ToListAsync();

            return _mapper.Map<List<StateBicingDto>>(entities);
        }
    }
}
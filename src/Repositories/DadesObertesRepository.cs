using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using AutoMapper;
using System.Linq;

namespace Repositories
{
    public class DadesObertesRepository : IDadesObertesRepository
    {
        private readonly ApiDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public DadesObertesRepository(ApiDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task BulkInsertAsync(
            List<LocationEntity> locations,
            List<HostEntity> hosts,
            List<StationEntity> stations,
            List<PortEntity> ports)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE location CASCADE");
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE host CASCADE");
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE station CASCADE");
                    await _dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE port CASCADE");

                    await _dbContext.Locations.AddRangeAsync(locations);
                    await _dbContext.SaveChangesAsync(); 
                
                    await _dbContext.Hosts.AddRangeAsync(hosts);
                    await _dbContext.SaveChangesAsync(); 
                    
                    await _dbContext.Stations.AddRangeAsync(stations);
                    await _dbContext.SaveChangesAsync(); 
                    
                    await _dbContext.Ports.AddRangeAsync(ports);
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

        public async Task<List<StationDto>> GetAllStations()
        {
            var entities = await _dbContext.Stations
                .Select(s => new StationEntity
                {
                    StationId = s.StationId,
                    StationLabel = s.StationLabel,
                    StationLatitude = s.StationLatitude,
                    StationLongitude = s.StationLongitude,
                    LocationId = s.LocationId
                })
                .ToListAsync();

            return _mapper.Map<List<StationDto>>(entities);
        }
    }
}
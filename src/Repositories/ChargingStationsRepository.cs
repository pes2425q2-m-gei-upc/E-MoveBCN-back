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
public class ChargingStationsRepository(ApiDbContext dbContext, IMapper mapper) : IChargingStationsRepository
{
  private readonly ApiDbContext _dbContext = dbContext;
  private readonly IMapper _mapper = mapper;

  public async Task BulkInsertAsync(
      List<LocationEntity> locations,
      List<HostEntity> hosts,
      List<StationEntity> stations,
      List<PortEntity> ports)
  {
    using var transaction = await this._dbContext.Database.BeginTransactionAsync().ConfigureAwait(false);
    try
    {
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE location CASCADE").ConfigureAwait(false);
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE host CASCADE").ConfigureAwait(false);
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE station CASCADE").ConfigureAwait(false);
      await this._dbContext.Database.ExecuteSqlRawAsync("TRUNCATE TABLE port CASCADE").ConfigureAwait(false);

      await this._dbContext.Locations.AddRangeAsync(locations).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await this._dbContext.Hosts.AddRangeAsync(hosts).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await this._dbContext.Stations.AddRangeAsync(stations).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await this._dbContext.Ports.AddRangeAsync(ports).ConfigureAwait(false);
      await this._dbContext.SaveChangesAsync().ConfigureAwait(false);

      await transaction.CommitAsync().ConfigureAwait(false);
    }
    catch (Exception)
    {
      await transaction.RollbackAsync().ConfigureAwait(false);
      throw;
    }
  }

  public async Task<List<ChargingStationDto>> GetAllChargingStations()
  {

    var query =
        from station in this._dbContext.Stations
        join location in this._dbContext.Locations
            on station.LocationId equals location.LocationId
        join host in this._dbContext.Hosts
            on location.LocationId equals host.LocationId
        join port in this._dbContext.Ports
            on station.StationId equals port.StationId into portsGroup
        select new ChargingStationDto
        {
          // Station
          StationId = station.StationId,
          StationLabel = station.StationLabel,
          StationLatitude = station.StationLatitude,
          StationLongitude = station.StationLongitude,

          // Location
          AddressString = location.AddressString,
          Locality = location.Locality,
          PostalCode = location.PostalCode,
          LocationLatitude = location.Latitude,
          LocationLongitude = location.Longitude,

          // Host
          HostId = host.HostId.ToString(),
          HostName = host.HostName,
          HostPhone = host.OperatorPhone,
          HostWebsite = host.OperatorWebsite,

          // Ports
          Ports = portsGroup.Select(p => new PortDto
          {
            PortId = p.PortId,
            StationId = p.StationId,
            ConnectorType = p.ConnectorType,
            PowerKw = p.PowerKw,
            PortStatus = p.Status,
            Reservable = p.Reservable,
            ChargingMechanism = p.ChargingMechanism,
            LastUpdated = p.LastUpdated
          }).ToList()
        };

    return await query.ToListAsync().ConfigureAwait(false);
  }
  public async Task<ChargingStationDto> GetChargingStationDetails(int ubicationId)
  {
    var query =
        from station in this._dbContext.Stations
        join location in this._dbContext.Locations
            on station.LocationId equals location.LocationId
        join host in this._dbContext.Hosts
            on location.LocationId equals host.LocationId
        join port in this._dbContext.Ports
            on station.StationId equals port.StationId into portsGroup
        where station.StationId == ubicationId.ToString(System.Globalization.CultureInfo.InvariantCulture)
        select new ChargingStationDto
        {
          // Station
          StationId = station.StationId,
          StationLabel = station.StationLabel,
          StationLatitude = station.StationLatitude,
          StationLongitude = station.StationLongitude,

          // Location
          AddressString = location.AddressString,
          Locality = location.Locality,
          PostalCode = location.PostalCode,
          LocationLatitude = location.Latitude,
          LocationLongitude = location.Longitude,

          // Host
          HostId = host.HostId.ToString(),
          HostName = host.HostName,
          HostPhone = host.OperatorPhone,
          HostWebsite = host.OperatorWebsite,

          // Ports
          Ports = portsGroup.Select(p => new PortDto
          {
            PortId = p.PortId,
            StationId = p.StationId,
            ConnectorType = p.ConnectorType,
            PowerKw = p.PowerKw,
            PortStatus = p.Status,
            Reservable = p.Reservable,
            ChargingMechanism = p.ChargingMechanism,
            LastUpdated = p.LastUpdated
          }).ToList()
        };

    return this._mapper.Map<ChargingStationDto>(await query.FirstOrDefaultAsync().ConfigureAwait(false));
  }
}

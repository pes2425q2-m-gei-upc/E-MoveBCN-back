using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entity;
using Microsoft.EntityFrameworkCore;
using Repositories.Interface;
using AutoMapper;
using System.Linq;
namespace Repositories;

public class DadesObertesRepository : IDadesObertesRepository
{
    private readonly ApiDbContext _Dbcontext;
    private readonly IMapper _mapper;
    public DadesObertesRepository(ApiDbContext Dbcontext, IMapper mapper)
    {
            _Dbcontext = Dbcontext;
            _mapper = mapper;
    }

    public async Task AddLocationAsync(LocationDto location)
    {
        var entity = new LocationEntity
        {
            LocationId = location.LocationId,
            NetworkBrandName = location.NetworkBrandName,
            OperatorPhone = location.OperatorPhone,
            OperatorWebsite = location.OperatorWebsite,
            Latitude = (float)location.Latitude,
            Longitude = (float)location.Longitude,
            AddressString = location.AddressString,
            Locality = location.Locality,
            PostalCode = location.PostalCode,
            LastUpdated = location.LastUpdated
        };

        _Dbcontext.LocationEntity.Add(entity);
        await _Dbcontext.SaveChangesAsync();
    }

    public async Task AddHostAsync(HostDto host)
    {
        var entity = new HostEntity
        {
            HostId = Guid.NewGuid(),
            HostName = host.HostName,
            HostAddress = host.HostAddress,
            HostLocality = host.HostLocality,
            HostPostalCode = host.HostPostalCode,
            OperatorPhone = host.OperatorPhone,
            OperatorWebsite = host.OperatorWebsite,
            LocationId = host.LocationId
        };

        _Dbcontext.Hosts.Add(entity);
        await _Dbcontext.SaveChangesAsync();
    }

    public async Task AddStationAsync(StationDto station)
    {
        var entity = new StationEntity
        {
            StationId = station.StationId,
            StationLabel = station.StationLabel,
            StationLatitude = station.StationLatitude,
            StationLongitude = station.StationLongitude,
            Reservable = station.Reservable,
            LocationId = station.LocationId
        };

        _Dbcontext.Stations.Add(entity);
        await _Dbcontext.SaveChangesAsync();
    }

    public async Task AddPortAsync(PortDto port)
    {
        var entity = new PortEntity
        {
            PortId = port.PortId,
            ConnectorType = port.ConnectorType,
            PowerKw = port.PowerKw,
            ChargingMechanism = port.ChargingMechanism,
            Status = port.PortStatus,
            LastUpdated = port.LastUpdated,
            StationId = port.StationId
        };

        _Dbcontext.Ports.Add(entity);
        await _Dbcontext.SaveChangesAsync();
    }

    public async Task<List<StationDto>> GetAllStations()
    {
        var entities = await _Dbcontext.Stations
            .Select(s => new StationEntity
            {
                StationId = s.StationId,
                StationLabel = s.StationLabel,
                StationLatitude = s.StationLatitude,
                StationLongitude = s.StationLongitude,
                Reservable = s.Reservable,
                LocationId = s.LocationId
            })
            .ToListAsync();

        return _mapper.Map<List<StationDto>>(entities);
    }
    
}
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
    private readonly ApiDbContext _context;
    private readonly IMapper _mapper;
    public DadesObertesRepository(ApiDbContext context, IMapper mapper)
    {
            _context = context;
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

        _context.LocationEntity.Add(entity);
        await _context.SaveChangesAsync();
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

        _context.Hosts.Add(entity);
        await _context.SaveChangesAsync();
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

        _context.Stations.Add(entity);
        await _context.SaveChangesAsync();
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

        _context.Ports.Add(entity);
        await _context.SaveChangesAsync();
    }

    public List<StationDto> GetAllStations()
    {
        var entities = _context.Stations
            .Select(s => new StationDto
            {
                StationId = s.StationId,
                StationLabel = s.StationLabel,
                StationLatitude = s.StationLatitude,
                StationLongitude = s.StationLongitude,
                Reservable = s.Reservable,
                LocationId = s.LocationId
            })
            .ToList();

        return _mapper.Map<List<StationDto>>(entities);
    }
    
}
using AutoMapper;
using Dto;
using Entity;
namespace Mapper;
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<StationEntity, StationDto>();
        CreateMap<LocationEntity, LocationDto>();
        CreateMap<HostEntity, HostDto>();
        CreateMap<PortEntity, PortDto>();
        CreateMap<StateBicingEntity, StateBicingDto>();
        CreateMap<BicingStationEntity, BicingStationDto>();
    }
}
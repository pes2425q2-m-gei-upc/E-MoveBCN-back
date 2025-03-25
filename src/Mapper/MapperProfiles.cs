using AutoMapper;
using Dto;
using Entity;
namespace Mapper;
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<UserEntity, UserDto>();
        CreateMap<StationEntity, StationDto>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationIdNavigation));
        CreateMap<LocationEntity, LocationDto>();
        CreateMap<HostEntity, HostDto>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.LocationIdNavigation));
        CreateMap<PortEntity, PortDto>()
            .ForMember(dest => dest.Station, opt => opt.MapFrom(src => src.StationIdNavigation));
        CreateMap<StateBicingEntity, StateBicingDto>()
            .ForMember(dest => dest.BicingStation, opt => opt.MapFrom(src => src.BicingStationIdNavigation));
        CreateMap<BicingStationEntity, BicingStationDto>();
    }
}
using System.Collections.Generic;
using System.Text.Json;
using AutoMapper;
using Dto.Bicing;
using Dto.Chat;
using Dto.Route;
using Dto.Ubication;
using Dto.User;
using Entity.Bicing;
using Entity.Chat;
using Entity.Route;
using Entity.Ubication;
using Entity.User;

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
    CreateMap<SavedUbicationEntity, SavedUbicationDto>();
    CreateMap<RouteEntity, RouteDto>()
    .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.UserIdNavigation))
    .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => DeserializeGeometry(src.GeometryJson)))
    .ForMember(dest => dest.Instructions, opt => opt.MapFrom(src => DeserializeInstructions(src.InstructionsJson)));
    CreateMap<PublishedRouteEntity, PublishedRouteDto>()
        .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.RouteIdNavigation));
    CreateMap<ChatEntity, ChatResponseDto>()
    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ChatId))
    .ForMember(dest => dest.RutaId, opt => opt.MapFrom(src => src.RouteId))
    .ForMember(dest => dest.HostId, opt => opt.MapFrom(src => src.User1Id))
    .ForMember(dest => dest.JoinerId, opt => opt.MapFrom(src => src.User2Id))
    .ForMember(dest => dest.User2, opt => opt.MapFrom(src => src.UserId2Navigation)) 
    .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.PublicRouteNavigation));
    CreateMap<MessageEntity, MessageDto>()
    .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.Message));

    CreateMap<MessageDto, MessageEntity>()
        .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.MessageText));


  }

  private static List<double[]> DeserializeGeometry(string json)
  {
    return JsonSerializer.Deserialize<List<double[]>>(json) ?? [];
  }

  private static List<RouteInstructionDto> DeserializeInstructions(string json)
  {
    return JsonSerializer.Deserialize<List<RouteInstructionDto>>(json) ?? [];
  }
}

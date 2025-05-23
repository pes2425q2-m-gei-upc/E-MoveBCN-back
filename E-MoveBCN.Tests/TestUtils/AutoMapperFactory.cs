using AutoMapper;
using Dto.Ubication;
using Dto.User;
using Entity.Ubication;
using Entity.User;
namespace TestUtils;
public static class AutoMapperFactory
{
  public static IMapper CreateMapper()
  {
    var config = new MapperConfiguration(cfg =>
    {
      cfg.CreateMap<UserEntity, UserDto>().ReverseMap();
      cfg.CreateMap<UserCreate, UserEntity>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore());
      cfg.CreateMap<SavedUbicationEntity, SavedUbicationDto>()
        .ForMember(dest => dest.AirQuality, opt => opt.Ignore());
      cfg.CreateMap<SavedUbicationDto, SavedUbicationEntity>();
    });

    config.AssertConfigurationIsValid();

    return config.CreateMapper();
  }
}

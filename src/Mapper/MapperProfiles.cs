using AutoMapper;
using Dto;
using Entity;
namespace Mapper;
public class MapperProfiles : Profile
{
    public MapperProfiles()
    {
        CreateMap<UserEntity, UserDto>();
    }
}
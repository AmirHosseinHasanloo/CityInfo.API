using AutoMapper;
using CityInfo.API.DTOs.Authenticate;
using CityInfo.API.Entites;

namespace CityInfo.API.Auto_Mapper_Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<AuthenticateRequestBody, CityInfoUser>();
    }
}
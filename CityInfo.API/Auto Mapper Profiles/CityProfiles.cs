using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CityInfo.API.Auto_Mapper_Profiles
{
    public class CityProfiles : Profile
    {
        public CityProfiles()
        {
            CreateMap<Entites.City, DTOs.CityWithoutPointOfIntrestDto>();
            CreateMap<Entites.City, DTOs.CitiesDTo>();
        }
    }
}

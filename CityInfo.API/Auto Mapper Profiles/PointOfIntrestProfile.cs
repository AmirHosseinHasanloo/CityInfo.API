using AutoMapper;
using CityInfo.API.DTOs;
using CityInfo.API.DTOs.CreationDTOs;
using CityInfo.API.DTOs.UpdateDTOs;
using CityInfo.API.Entites;

namespace CityInfo.API.Auto_Mapper_Profiles
{
    public class PointOfIntrestProfile : Profile
    {
        public PointOfIntrestProfile()
        {
            CreateMap<PointOfIntrest, PointOfIntrestDTo>();
            CreateMap<CreationDToPointOfIntrest, PointOfIntrest>();
            CreateMap<PointOfIntrest, UpdateDToPointOfIntrest>();
        }
    }
}

using CityInfo.API.DTOs.Authenticate;
using CityInfo.API.Entites;

namespace CityInfo.API.Repositories;

public interface IUserRepository
{
    Task AddUser (CityInfoUser user);
}
using CityInfo.API.Entites;

namespace CityInfo.API.Repositories;

public interface IValidateRepository
{
    CityInfoUser ValidateUserCredentials(string? userName, string? password);
}
using CityInfo.API.DTOs;
using CityInfo.API.Entites;

namespace CityInfo.API.Repositories
{
    public interface ICityInfoRepository
    {
        Task<IEnumerable<CityWithoutPointOfIntrestDto>> GetAllCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool IncludepointOfIntrest);
        Task<bool> IsCityExists(int cityId);
        Task<bool> IsPointOfIntrestExists(int cityId, int pointOfIntrest);
        Task<IEnumerable<PointOfIntrest>> GetPointOfIntrestsForCityAsync(int cityId);
        Task<PointOfIntrest> GetPointOfIntrestForCityAsync(int cityId, int pointOfIntrestId);
        Task AddPointOfIntrestToCityAsync(int cityId, PointOfIntrest pointOfIntrest);
        Task<bool> DeletePointOfIntrest(int cityId, int pointOfIntrestId);
        Task<bool> DeleteCityWithId(int cityId);
        Task<bool> SaveChangesAsync();
    }
}

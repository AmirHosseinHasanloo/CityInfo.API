using CityInfo.API.DbContexts;
using CityInfo.API.DTOs;
using CityInfo.API.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace CityInfo.API.Repositories.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<IEnumerable<CityWithoutPointOfIntrestDto>> GetAllCitiesAsync()
        {
            return await _context.Cities
                .Select(c => new CityWithoutPointOfIntrestDto()
                {
                    description = c.Description,
                    Id = c.Id,
                    name = c.Name,
                }).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool IncludepointOfIntrest)
        {
            if (IncludepointOfIntrest)
            {
                return await _context.Cities.Include(c => c.PointOfIntrests)
                    .FirstOrDefaultAsync<City>(c => c.Id == cityId);
            }
            return await _context.FindAsync<City>(cityId);
        }

        public async Task<PointOfIntrest> GetPointOfIntrestForCityAsync(int cityId, int pointOfIntrestId)
        {
            return await _context.PointOfIntrests
                .SingleAsync(p => p.Id == pointOfIntrestId && p.CityId == cityId);

        }

        public async Task<IEnumerable<PointOfIntrest>> GetPointOfIntrestsForCityAsync(int cityId)
        {
            return await _context.PointOfIntrests
                .Where(p => p.CityId == cityId).ToListAsync();
        }

        public async Task<bool> IsCityExists(int cityId)
        {
            return await _context.Cities.AnyAsync(c => c.Id == cityId);
        }

        public async Task<bool> IsPointOfIntrestExists(int cityId, int pointOfIntrest)
        {
            var checkPointOfIntrest = await _context.PointOfIntrests
                .AnyAsync(c => c.Id == pointOfIntrest && c.CityId == cityId);


            return checkPointOfIntrest;
        }

        public async Task AddPointOfIntrestToCityAsync(int cityId, PointOfIntrest pointOfIntrest)
        {
            var city = await GetCityAsync(cityId, false);

            if (city != null)
            {
                city.PointOfIntrests.Add(pointOfIntrest);
            }

        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() > 0);
        }

        public async Task<bool> DeletePointOfIntrest(int cityId, int pointOfIntrestId)
        {
            var city = await GetCityAsync(cityId, true);

            if (city == null)
            {
                return false;
            }


            var pointOfIntrest = city.PointOfIntrests
                .SingleOrDefault(p => p.Id == pointOfIntrestId);
            if (pointOfIntrest != null)
            {
                // delete and save changes to db
                _context.Remove(pointOfIntrest);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> DeleteCityWithId(int cityId)
        {
            //get city async without point of intrest
            var city = await GetCityAsync(cityId, false);

            // check the city if be null return false tp presentation layer
            if (city == null)
            {
                return false;
            }

            // if city dont be null we remove the city and save changes to db 
            // and return true result to presentation layer
            _context.Remove(city);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}

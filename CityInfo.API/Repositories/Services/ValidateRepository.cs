using CityInfo.API.DbContexts;
using CityInfo.API.Entites;

namespace CityInfo.API.Repositories.Services;

public class ValidateRepository : IValidateRepository
{
    private readonly CityInfoContext _context;

    public ValidateRepository(CityInfoContext context)
    {
        _context = context;
    }

    public CityInfoUser ValidateUserCredentials(string? userName, string? password)
    {
        var user = _context.CityUsers
            .SingleOrDefault(u => u.userName == userName && u.password == password);

        if (user == null)
        {
            return null;
        }

        return user;
    }
}
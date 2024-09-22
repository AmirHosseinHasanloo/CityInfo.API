using CityInfo.API.DbContexts;
using CityInfo.API.Entites;

namespace CityInfo.API.Repositories.Services;

public class UserRepository : IUserRepository
{
    private readonly CityInfoContext _context;

    public UserRepository(CityInfoContext context)
    {
        _context = context;
    }

    public async Task AddUser(CityInfoUser user)
    {
        await _context.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}
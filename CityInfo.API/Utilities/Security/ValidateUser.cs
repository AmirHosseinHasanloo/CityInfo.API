using CityInfo.API.DbContexts;
using CityInfo.API.Entites;

namespace CityInfo.API.Utilities.Security
{
    public class ValidateUser
    {
        CityInfoContext _context;
        public ValidateUser(CityInfoContext context)
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
}

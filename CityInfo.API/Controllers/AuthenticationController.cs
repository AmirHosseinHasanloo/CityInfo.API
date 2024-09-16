using CityInfo.API.DbContexts;
using CityInfo.API.DTOs.Authenticate;
using CityInfo.API.Utilities.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]

    public class AuthenticationController : ControllerBase
    {
        private CityInfoContext _context;
        private ValidateUser _validateUser;
        private IConfiguration _configuration;

        public AuthenticationController(CityInfoContext context, ValidateUser validateUser,
            IConfiguration configuration)
        {
            _context = context;
            _validateUser = validateUser;
            _configuration = configuration;
        }

        [HttpPost("Authentication")]
        public ActionResult<string> Authenticate(AuthenticateRequestBody authenticateRequestBody)
        {
            var user = _validateUser
                .ValidateUserCredentials(authenticateRequestBody.userName
                , authenticateRequestBody.password);

            if (user == null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey
                (
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
                );

            return Ok();
        }
    }
}

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CityInfo.API.DbContexts;
using CityInfo.API.DTOs.Authenticate;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using CityInfo.API.Entites;
using CityInfo.API.Repositories;

namespace CityInfo.API.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private CityInfoContext _context;
        private IValidateRepository _validateUser;
        private IConfiguration _configuration;
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public AuthenticationController(CityInfoContext context, IValidateRepository validateUser,
            IConfiguration configuration, IMapper mapper, IUserRepository userRepository)
        {
            _context = context;
            _validateUser = validateUser;
            _configuration = configuration;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("Authentication")]
        public ActionResult<string> Authenticate(AuthenticateRequestBody authenticateRequestBody)
        {
            var user = _validateUser
                .ValidateUserCredentials(authenticateRequestBody.userName
                    , authenticateRequestBody.password);

            if (user != null)
            {
                return BadRequest();
            }

            var securityKey = new SymmetricSecurityKey
            (
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"])
            );
            var signingCredentials =
                new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var mappedUser = _mapper.Map<CityInfoUser>(authenticateRequestBody);
// default values
            mappedUser.City = "karaj";
            mappedUser.firstName = "default";
            mappedUser.lastName = "default";

            _userRepository.AddUser(mappedUser);

            var cliamsForToken = new List<Claim>()
            {
                new Claim("userId", mappedUser.UserId.ToString()),
                new Claim("userName", mappedUser.userName.ToString()),
                new Claim(ClaimTypes.Country, mappedUser.City)
            };

            var jwtSecurityToken = new JwtSecurityToken
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                cliamsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials
            );
            var TokenForReturn = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);
            return Ok(TokenForReturn);
        }
    }
}
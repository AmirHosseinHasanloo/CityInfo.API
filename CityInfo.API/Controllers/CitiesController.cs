using AutoMapper;
using CityInfo.API.DTOs;
using CityInfo.API.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CityInfo.API.Controllers
{
    [Route("api/Cities")]
    [Authorize]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;
        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ??
                throw new ArgumentException(nameof(cityInfoRepository));

            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfIntrestDto>>> GetCities()
        {
            var result = await _cityInfoRepository.GetAllCitiesAsync();

            // map data with mapper and return it

            return Ok
                (
                _mapper.Map<IEnumerable<CityWithoutPointOfIntrestDto>>(result)
                );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CitiesDTo>> GetCity(int id
            , bool includePointOfIntrest = false)
        {
            var city = await _cityInfoRepository.GetCityAsync(id, includePointOfIntrest);

            if (city == null)
            {
                return NotFound();
            }

            if (includePointOfIntrest)
            {
                return Ok
              (
              _mapper.Map<CitiesDTo>(city)
              );
            }

            return Ok
                (
                _mapper.Map<CityWithoutPointOfIntrestDto>(city)
                );
        }



        [HttpGet("GetName")]
        public ActionResult<string> GetAmirHossein()
        {
            return Ok("AmirHossein");
        }
    }
}

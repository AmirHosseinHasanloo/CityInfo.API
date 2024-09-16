using AutoMapper;
using CityInfo.API.DTOs;
using CityInfo.API.DTOs.CreationDTOs;
using CityInfo.API.DTOs.UpdateDTOs;
using CityInfo.API.Repositories;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointOfIntrestController : ControllerBase
    {
        private readonly ILogger<PointOfIntrestController> _logger;
        private readonly IMailService _localMailService;
        private readonly CitiesDataStore _citiesDataStore;
        private readonly IMapper _mapper;

        private readonly ICityInfoRepository _cityRepository;

        public PointOfIntrestController(ILogger<PointOfIntrestController> logger,
            IMailService localMailService
            , CitiesDataStore citiesDataStore, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            // if logger is null throw axception for it .
            _logger = logger ??
                throw new ArgumentNullException(nameof(logger));

            //if localMailService is null throw exception for it .
            _localMailService = localMailService ??
                throw new ArgumentNullException(nameof(localMailService));

            //if citiesDataStore is null throw exception for it .
            _citiesDataStore = citiesDataStore ??
                throw new ArgumentNullException(nameof(citiesDataStore));

            //if cityInfoRepository is null throw exception for it .
            _cityRepository = cityInfoRepository ??
                throw new ArgumentNullException(nameof(cityInfoRepository));

            //if mapper is null throw exception for it .
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfIntrestDTo>>>
            GetPointsOfInterest(int cityId)
        {
            if (await _cityRepository.IsCityExists(cityId) == false)
            {
                // log to console
                _logger.LogInformation($"There is no city with id {cityId}");

                return NotFound("There is no city with this id !");
            }


            var pointsOfIntrest =
                await _cityRepository.GetPointOfIntrestsForCityAsync(cityId);


            if (pointsOfIntrest.Any())
            {
                return NoContent();
            }


            return Ok(_mapper.Map<IEnumerable<PointOfIntrestDTo>>(pointsOfIntrest));
        }

        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterest")]
        public async Task<ActionResult<PointOfIntrestDTo>> GetPointOfInterest(
             int cityId, int pointOfInterestId
             )
        {
            if (!await _cityRepository.IsPointOfIntrestExists(cityId, pointOfInterestId))
            {
                // log
                _logger.LogInformation("There is no content with this information");

                // return not found and information
                return NotFound("There is no content with this information");
            }

            return Ok(_mapper.Map<PointOfIntrestDTo>(
              await _cityRepository.GetPointOfIntrestForCityAsync(cityId, pointOfInterestId)));
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<PointOfIntrestDTo>> CreatePointOfInterest(
           int cityId,
           CreationDToPointOfIntrest pointOfInterest
           )
        {
            if (!await _cityRepository.IsCityExists(cityId))
            {
                // log
                _logger.LogInformation($"there is no content with city id {cityId}");

                // show it to client
                return NotFound("There is no content with this information!");
            }

            var mapedPointOfIntrest = _mapper.Map<Entites.PointOfIntrest>(pointOfInterest);

            await _cityRepository
                .AddPointOfIntrestToCityAsync(cityId, mapedPointOfIntrest);
            await _cityRepository.SaveChangesAsync();


            // map to dto for return
            var createdPoint = _mapper.Map<PointOfIntrestDTo>(mapedPointOfIntrest);

            return CreatedAtRoute("GetPointOfInterest", new
            {
                cityId = cityId
                ,
                pointOfInterestId = createdPoint.Id
            }, createdPoint);
        }
        #endregion

        #region Put 
        [HttpPut("{pointId}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointId
            , UpdateDToPointOfIntrest pointOfIntrest)
        {
            var city = _citiesDataStore.cities
                .FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var point = city.PointOfIntrests.FirstOrDefault(p => p.Id == pointId);

            if (point is null)
            {
                return NotFound();
            }

            point.Name = pointOfIntrest.Name;
            point.Description = pointOfIntrest.Description;

            return NoContent();
        }

        #endregion

        #region Edit with patch
        [HttpPatch("{pointofintrestId}")]
        public async Task<ActionResult> PartiallyUpdatePointOfIntrest(int cityId,
            int pointofintrestId, JsonPatchDocument<UpdateDToPointOfIntrest> patchDucument)
        {
            if (!await _cityRepository.IsCityExists(cityId))
            {
                return NotFound();
            }

            var pointOfintrestEntity = await _cityRepository
                .GetPointOfIntrestForCityAsync(cityId, pointofintrestId);

            if (pointOfintrestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterstToPatch = _mapper.Map<UpdateDToPointOfIntrest>(pointOfintrestEntity);

            patchDucument.ApplyTo(pointOfInterstToPatch, modelState: ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterstToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(pointOfInterstToPatch, pointOfintrestEntity);

            await _cityRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion

        #region HttpDelete
        [HttpDelete("{pointOfIntrestId}")]
        public async Task<ActionResult> DeletePointOfIntrest(int cityId, int pointOfIntrestId)
        {
            bool point =
               await _cityRepository.DeletePointOfIntrest(cityId, pointOfIntrestId);

            if (!point)
            {
                _localMailService.Send("we cant remove", $"there is no point of intrest with these info :" +
                    $" CityId= {cityId} , pointOfIntrestId= {pointOfIntrestId}");
                return BadRequest();
            }

            _localMailService.Send("we remove", $"there is point of intrest with these info :" +
                   $" CityId= {cityId} , pointOfIntrestId= {pointOfIntrestId} Deleted right now!");

            return NoContent();
        }

        #endregion

    }
}

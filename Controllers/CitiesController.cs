using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            var cityToReturn= CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }

        [HttpPost]
        public ActionResult<CityDto> AddCity(CreationDto city)
        {
            //get max id
            var cityId = CitiesDataStore.Current.Cities.Max(p => p.Id);
            
            var finalCity = new CityDto();
            finalCity.Id = ++cityId;
            finalCity.Name = city.Name;
            finalCity.Description= city.Description;

            CitiesDataStore.Current.Cities.Add(finalCity);
            return Ok(finalCity);
        }

        [HttpPut("{cityId}")]
        public ActionResult<CityDto> UpdateCity(int cityId, CreationDto city)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            cityToReturn.Name = city.Name;
            cityToReturn.Description = city.Description;
            return Ok(cityToReturn);
        }

        [HttpPatch("{cityId}")]
        public ActionResult PartiallyUpdateCity(int cityId, JsonPatchDocument<CreationDto> patchDocument)
        {
            //city is not found
            var existingCity = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (existingCity == null)
            {
                return NotFound();
            }
            //transform the city to a creationDTO
            var cityToPatch = new CreationDto()
            {
                Name = existingCity.Name,
                Description = existingCity.Description
            };

            patchDocument.ApplyTo(cityToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(cityToPatch))
            {
                return BadRequest(ModelState);
            }

            existingCity.Name = cityToPatch.Name;
            existingCity.Description = cityToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}")]
        public ActionResult DeleteCity(int cityId)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            CitiesDataStore.Current.Cities.Remove(city);
            return NoContent();
;        }
    }
}

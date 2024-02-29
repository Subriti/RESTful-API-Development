using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            //parent city is not found
            var city= CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id== cityId);
            if(city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterests);
        }

        [HttpGet("{pointOfInterestId}", Name= "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId) 
        {
            //parent city is not found
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //find point of interest
            var pointOfInterest= city.PointOfInterests.FirstOrDefault(c=> c.Id== pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            //automatically checked

            /*if(!ModelState.IsValid){
                return BadRequest();
            }*/

            //parent city is not found
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            //get max id
            var pointOfInterestId = CitiesDataStore.Current.Cities.SelectMany(c => c.PointOfInterests).Max(p=> p.Id);
            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++pointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointOfInterests.Add(finalPointOfInterest);
            //return Ok(finalPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest", new {
                cityId= cityId,
                pointOfInterest= finalPointOfInterest.Id
            }, finalPointOfInterest);
        }

        //use put to change all attributes
        [HttpPut("{pointOfInterestId}")]
        public ActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForCreationDto pointOfInterest)
        {
            //parent city is not found
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            
            //find point of interest
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();

        }

        //use patch to change single to few attributes; with operation
        [HttpPatch("{pointOfInterestId}")]
        public ActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId, JsonPatchDocument<PointOfInterestForCreationDto> patchDocument)
        {
            //parent city is not found
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //find point of interest
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //transform the point of interest from store to a pointOfInterestUPdateDTO
            var pointOfInterestToPatch = new PointOfInterestForCreationDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public ActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            //parent city is not found
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            //find point of interest
            var pointOfInterestFromStore = city.PointOfInterests.FirstOrDefault(c => c.Id == pointOfInterestId);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterests.Remove(pointOfInterestFromStore);
            return NoContent();
        }
    }
}

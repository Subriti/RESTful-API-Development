﻿using FindClosestRestaurantNearMe;
using Microsoft.AspNetCore.Mvc;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantController : ControllerBase
    {
        RestaurantFinder restaurantFinder = new();

        [HttpGet("userLocation")]
        public ActionResult<Geocoordinates> GetUserLocation()
        {
            var userLocation = GeolocationService.GetCurrentUserLocation().Result;
            var geoCoordinates = new Geocoordinates(userLocation.Latitude, userLocation.Longitude);
            return Ok(geoCoordinates);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Restaurants>> GetRestaurants()
        {
            var restaurants= restaurantFinder.FindRestaurants();
            return Ok(restaurants);
        }

        [HttpGet("nearestRestaurant")]
        public ActionResult<Restaurants> GetNearestRestaurant()
        {
            var restaurants = restaurantFinder.FindRestaurants();
            if (restaurants == null)
            {
                return NotFound();
            }
            var nearestRestaurant = restaurantFinder.FindNearestRestaurant(restaurants);
            return Ok(nearestRestaurant);
        }
    }
}

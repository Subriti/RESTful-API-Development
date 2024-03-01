using Fetching_Weather;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("fetchWeatherData")]
        public ActionResult<WeatherDto> GetCurrentWeatherData([Required]string city, string? longitude)
        {
            WeatherFetchService weatherFetchService = new WeatherFetchService();
            var weather = weatherFetchService.FetchData(city, longitude).Result;
            if (weather == null)
            {
                return NotFound();
            }
            return Ok(weather);
        }
    }
}
using Fetching_Weather;
using Microsoft.AspNetCore.Mvc;

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

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

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
        public ActionResult<WeatherDto> GetCurrentWeatherData(string city, string? latitude)
        {
            WeatherFetchService weatherFetchService = new WeatherFetchService();
            var weather = weatherFetchService.FetchData(city, latitude).Result;
            if (weather == null)
            {
                return NotFound();
            }
            return Ok(weather);
        }
    }
}

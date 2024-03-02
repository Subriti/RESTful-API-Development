using Newtonsoft.Json.Linq;
namespace Fetching_Weather
{
    public class WeatherFetchService
    {
        public async Task<WeatherDto?> FetchData(string city, string? longitude)
        {
            using HttpClient client = new();
            if (longitude is null)
            {
                longitude = "0";
            }
            return await ProcessRepositoriesAsync(client, city, longitude);
        }

        private static async Task<WeatherDto?> ProcessRepositoriesAsync(HttpClient client, String city, String longitude)
        {
            var baseURL = $"https://api.weatherapi.com/v1/current.json?q={city}" +
                $"&lang={longitude}&key=044baa979e9c442ca4095316241202";

            try
            {
                var result = await client.GetStringAsync(baseURL);
                JObject jsonObject = JObject.Parse(result);

                WeatherDto weather = new();
                weather.Name = (string)jsonObject["location"]["name"];
                weather.Temp_C = (string)jsonObject["current"]["temp_c"];
                weather.Temp_F= (string)jsonObject["current"]["temp_f"];
                weather.LastUpdated= (string)jsonObject["current"]["last_updated"];
                weather.Condition= (string)jsonObject["current"]["condition"]["text"];

                return weather;
            }
            catch (Exception ex)
            {
                return new WeatherDto();
            }
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.Controllers
{
    [ApiController]
    [Route("api/ottInfo")]
    public class OTTController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<OTTModelDto>>> GetMovieInfo([FromQuery] OTTModel model)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://ott-details.p.rapidapi.com/advancedsearch?start_year={model.start_year}&end_year={model.end_year}&min_imdb={model.min_imdb}&max_imdb={model.max_imdb}&genre={model.genre}&language={model.language}&type={model.type}&sort={model.sort}&page={model.page}"),
                Headers =
                {
                    { "X-RapidAPI-Key", "e57a49aeeamshce97655db6baa20p129a29jsn9eb15225a2b6" },
                    { "X-RapidAPI-Host", "ott-details.p.rapidapi.com" },
                },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();          

                JObject jsonObject = JObject.Parse(body);
                var results = jsonObject["results"];

                List<OTTModelDto> listModel = new List<OTTModelDto>();

                foreach (var result in results)
                {
                    OTTModelDto modeldto = new();
                    modeldto.title = (string)result["title"];
                    modeldto.released_year = (int)result["released"];

                    var rating = double.TryParse((string)result["imdbrating"], out double ratingss);
                    modeldto.imdbRating = ratingss;
                    modeldto.type = (string)result["type"];

                    var genre = result["genre"];

                    List<string> list = new();
                    for (int i=0; i<genre.Count(); i++)
                    {
                        list.Add(genre[i].ToString());
                    }
                    modeldto.genre= list;
                    listModel.Add(modeldto);
                }
                return Ok(listModel);
            }
        }
    }
}

using System.Text.Json.Serialization;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class OTTModelDto
    {
        [property: JsonPropertyName("released")]
        public int released_year { get; set; }
        [property: JsonPropertyName("genre")]
        public List<string> genre { get; set; }
        [property: JsonPropertyName("type")]
        public string? type { get; set; } /// <summary>
                                          /// { movie, show }
                                          /// </summary>
        [property: JsonPropertyName("title")] 
        public string? title { get; set; } /// <summary>
                                           /// { highestrated, lowestrated, latest, oldest }
                                           /// </summary>
        [property: JsonPropertyName("imdbrating")]
        public double? imdbRating { get; set; } = 0.0D;
    }
}

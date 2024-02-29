using System.Text.Json.Serialization;

namespace Fetching_Weather
{
    public class WeatherDto
    { 
        [property: JsonPropertyName("name")]
        public string Name { get; set; }
        [property: JsonPropertyName("temp_c")]
        public string temp_c { get; set; }
        [property: JsonPropertyName("temp_f")]
        public string temp_f { get; set; }
        [property: JsonPropertyName("text")]
        public string text { get; set; }
        [property: JsonPropertyName("last_updated")]
        public string lastUpdated { get; set; }
    }
}

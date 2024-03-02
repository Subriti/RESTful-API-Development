using System.Text.Json.Serialization;

namespace Fetching_Weather
{
    public class WeatherDto
    { 
        [property: JsonPropertyName("name")]
        public string Name { get; set; }
        [property: JsonPropertyName("temp_c")]
        public string Temp_C { get; set; }
        [property: JsonPropertyName("temp_f")]
        public string Temp_F { get; set; }
        [property: JsonPropertyName("text")]
        public string Condition { get; set; }
        [property: JsonPropertyName("last_updated")]
        public string LastUpdated { get; set; }
    }
}

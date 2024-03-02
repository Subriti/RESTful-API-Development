namespace RESTful_API__ASP.NET_Core.Models
{
    public class OTTModel
    {
        public int start_year { get; set; } = 2020;
        public int end_year { get; set; } = 2023;
        public int? min_imdb { get; set; }
        public int? max_imdb { get; set; }
        public string? genre { get; set; }
        public string? language { get; set; }
        public string? type { get; set; } /// <summary>
                                         /// { movie, show }
                                         /// </summary>
        public string? sort { get; set; } /// <summary>
                                         /// { highestrated, lowestrated, latest, oldest }
                                         /// </summary>

        public int page { get; set; } = 1;
    }
}

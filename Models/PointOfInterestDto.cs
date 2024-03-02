using System.ComponentModel.DataAnnotations.Schema;

namespace RESTful_API__ASP.NET_Core.Models
{
    public class PointOfInterestDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        public CityDto? City { get; set; }
    }
}

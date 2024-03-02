namespace RESTful_API__ASP.NET_Core.Models
{
    public class Geocoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Geocoordinates(double latitude, double longitude) { 
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}

﻿namespace FindClosestRestaurantNearMe
{
    public class Restaurants
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Distance { get; set; }

        public Restaurants(string name, double latitude, double longitude)
        {
            this.Name = name;
            this.Latitude = latitude;
            this.Longitude = longitude;
        }
    }
}

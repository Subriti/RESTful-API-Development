using FindClosestRestaurantNearMe;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core
{
    public sealed class DBContext:DbContext
    {
        public DbSet<CityDto> Cities { get; set; }
        public DbSet<PointOfInterestDto> PointOfInterests { get; set;}  

        public DBContext(DbContextOptions<DBContext> options) : base(options) {
            Database.EnsureCreated();
        }

    }
}

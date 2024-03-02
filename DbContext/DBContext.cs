using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.Models;

namespace RESTful_API__ASP.NET_Core.DbContext
{
    public sealed class DBContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<CityDto> Cities { get; set; }
        public DbSet<PointOfInterestDto> PointOfInterests { get; set; }
        public DbSet<Users> Users { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Users>()
           .HasOne(u => u.City)
           .WithMany()
           .HasForeignKey(u => u.CityId);*/

            modelBuilder.Entity<CityDto>()
                .HasData(
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park."
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never ready."
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower."
                });


            modelBuilder.Entity<PointOfInterestDto>()
                .HasData(
                    new PointOfInterestDto()
                    {
                        Id = 1,
                        CityId = 1,
                        Name = "Central Park",
                        Description = "The most visited urban park in the United States"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 2,
                        CityId = 1,
                        Name = "Empire State Building",
                        Description = "A 102-storey skyscraper located in the Midtown Manhattan"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 3,
                        CityId = 2,
                        Name = "Cathedral of our lady",
                        Description = "A gothic style cathedral, conceived by architects"
                    },
                    new PointOfInterestDto()
                    {
                        Id = 4,
                        CityId = 2,
                        Name = "Antwerp Central Station",
                        Description = "The first finest example of railway architecture in Belgium."
                    }, new PointOfInterestDto()
                    {
                        Id = 5,
                        CityId = 3,
                        Name = "Eiffel Tower",
                        Description = "A wrought iron lattice tower on the Champ de Mars."
                    },
                    new PointOfInterestDto()
                    {
                        Id = 6,
                        CityId = 3,
                        Name = "The Louvre",
                        Description = "The world's largest museum."
                    }
                );

            modelBuilder.Entity<Users>().HasData(
                new Users("Subriti Aryal", "aryalsubriti@gmail.com", "Subriti123", 1) { Id = -1 },
                new Users("Ashish Jaiswal", "ashishjaiswal@gmail.com", "Ashish123", 1) { Id = -2 },
                new Users("Prakhyat Shrestha", "prakhyat123@gmail.com", "Prakhyat123", 3) { Id = -3 },
                new Users("Apoorva Basnet", "apoo@gmail.com", "Apoorva123", 2) { Id = -4 });

            base.OnModelCreating(modelBuilder);
        }
    }
}

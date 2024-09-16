using CityInfo.API.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Karaj")
                {
                    Description = "Here is karaj",
                    Id = 1,
                },
                new City("Tehran")
                {
                    Description = "Here is Tehran",
                    Id = 2,
                },
                 new City("Mashad")
                 {
                     Description = "Here is Mashad",
                     Id = 3,
                 }

                );

            modelBuilder.Entity<PointOfIntrest>().HasData(
                new PointOfIntrest("Baraghan")
                {
                    Id = 1,
                    CityId = 1,
                },
                  new PointOfIntrest("Talaghan")
                  {
                      Id = 2,
                      CityId = 1
                  },
                    new PointOfIntrest("Lavasan")
                    {
                        Id = 3,
                        CityId = 2
                    },
                      new PointOfIntrest("Azadi")
                      {
                          Id = 4,
                          CityId = 2
                      },
                        new PointOfIntrest("Idont know abad")
                        {
                            Id = 5,
                            CityId = 3
                        },
                          new PointOfIntrest("Idont know abad 2")
                          {
                              Id = 6,
                              CityId = 3
                          }

                );
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfIntrest> PointOfIntrests { get; set; } = null!;
        public DbSet<CityInfoUser> CityUsers { get; set; } = null!;
    }
}

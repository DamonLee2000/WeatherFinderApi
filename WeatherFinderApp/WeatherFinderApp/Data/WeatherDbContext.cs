using Microsoft.EntityFrameworkCore;
using WeatherFinderApp.Models;

namespace WeatherFinderApp.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) // this is the constructor for the WeatherDbContext class. This method is called when an isntance(object) of a class is created.
            : base(options) // call to the constructor base class ('DbContext'). ensures when a WeatherDbContext object is created, constructor of the base class "DbContext" is also called.
        {
        }

        // this represents the table of WeatherData entities from Models\WeatherData.cs
        public DbSet<WeatherData> WeatherTable { get; set; }
    }
}


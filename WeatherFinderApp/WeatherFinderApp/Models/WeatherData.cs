namespace WeatherFinderApp.Models
{
    // this class represents a single Weather entity. Each weather entity will be stored in WeatherTable, found in Data\WeatherDbContext.cs
    public class WeatherData // this is the constructor for the db
    {
        public int Id { get; set; }
        public string CityName { get; set; }
        public double Temperature { get; set; }
    }
}

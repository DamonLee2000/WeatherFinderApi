﻿using Microsoft.AspNetCore.Mvc;
using WeatherFinderApp.Data;

namespace WeatherFinderApp.Controllers
{
    [ApiController] // indicates that this class is an API controller
    [Route("api/[controller]")] // defines the route/ "[controller]" is replaced with the name of the controller, in this case it is "Weather."
    public class WeatherController : ControllerBase
    {
        // this WeatherDbContext field is only in this class and cannot be changed after object is constructed. conventionally using "_" shows it's a private field.
        private readonly WeatherDbContext _context; // will allows WeatherController to interact with the database.

        // constructor
        public WeatherController(WeatherDbContext context) // takes an instance of WeatherDbContext. When an instance of WeatherController is created, it has access to WeatherDbContext.
        {
            _context = context;
        }

        [HttpGet] // attribute specifying that this ACTION METHOD handles https GET requests.
        public IActionResult GetWeatherData() // this method handles GET requests to retrieve weather data.
        {
            // database interaction:
            var weatherData = _context.WeatherTable.ToList(); // uses "_context" (instance of WeatherDbContext) to query database for all entries in WeatherTable. Converts results to a list.
            return Ok(weatherData); // returns HTTP 200 ok response and weather data.
        }
    }
}
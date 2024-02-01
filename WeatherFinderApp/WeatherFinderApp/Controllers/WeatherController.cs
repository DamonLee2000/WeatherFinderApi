using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WeatherFinderApp.Data;
using WeatherFinderApp.Models;

namespace WeatherFinderApp.Controllers
{
    //[ApiController] // indicates that this class is an API controller
    //[Route("api/[controller]")] // defines the route/ "[controller]" is replaced with the name of the controller, in this case it is "Weather."

    //[Route("[controller]")]
    [Route("Weather")]
    public class WeatherController : Controller//Base
    {
        // this WeatherDbContext field is only in this class and cannot be changed after object is constructed. conventionally using "_" shows it's a private field.
        private readonly WeatherDbContext _context; // will allows WeatherController to interact with the database.

        // constructor
        public WeatherController(WeatherDbContext context) // takes an instance of WeatherDbContext. When an instance of WeatherController is created, it has access to WeatherDbContext.
        {
            _context = context;
        }

        
        [HttpGet] // This attribute specifies that this ACTION METHOD handles HTTP GET requests.
        public IActionResult Index()
        {
            return View(); // return a view named "Index.cshtml".
        }
        

        [HttpPost] // attribute specifying that this ACTION METHOD. Use post because we post data from user input to be processed by my server, which will then interact with OpenWeatherMap API.
        // parameters: ZipCode and Country name from the model WeatherInput. WeatherInput is constructed through a form found in Index.cshtml
        public async Task<IActionResult> GetWeatherData([FromQuery] WeatherInput weatherInput) // this method handles user input to retrieve weather data.
        {
            // get user input
            // use weatherInput to get data from the form
            var zipcode = weatherInput.Zipcode;
            var country = weatherInput.Country;

            var apiKey = "237653991af597a3b573e986887b2984";
            var geoApiUrl = "http://api.openweathermap.org/geo/1.0/zip?zip={zipcode},{country}&appid={API key}";

            try
            {
                using (var httpClient = new HttpClient())
                {
                    // http get request
                    var geoApiResposne = await httpClient.GetStringAsync(geoApiUrl); // this needs to be a string because JsonSerializer.Deserialize requries a string representing json data. change GetAsync to GetStringAsync

                    if (!string.IsNullOrEmpty(geoApiResposne)) // if geoApiResponse has something in it. (geoApiResponse is a string because of the GetStringAsync method).
                    {
                        var geoApiData = JsonSerializer.Deserialize<List<GeoApiResponse>>(geoApiResposne);

                        if (geoApiData.Count > 0)
                        {
                            var latitude = geoApiData[0].Latitude;
                            var longitude = geoApiData[0].Longitude;

                            Console.WriteLine(latitude);
                            Console.WriteLine(longitude);
                        }
                    }
                    else
                    {
                        Console.WriteLine("GeoAPI response is empty");
                    }
                }
            }
            catch (HttpRequestException ex) // error http GET
            {
                Console.WriteLine("HTTP GET ERROR: {ex.Message}");
                return BadRequest("Error getting data from OpenWeatherMap API");
            }
            catch (JsonException ex) // error deserializing API response
            {
                Console.WriteLine("JSON deserialization error: {ex.Message}");
                return BadRequest("Error parsing JSON string");
            }

            // database interaction:
            //var weatherData = _context.WeatherTable.ToList(); // uses "_context" (instance of WeatherDbContext) to query database for all entries in WeatherTable. Converts results to a list.
            return Ok("done"); // returns HTTP 200 ok response and weather data.
        }
    }
}

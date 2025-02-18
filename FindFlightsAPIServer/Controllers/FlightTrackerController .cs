using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FindFlightsAPI.Controllers
{
    [Route("api/flighttracker")]
    [ApiController]
    public class FlightTrackerController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public FlightTrackerController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }



        // http://localhost:5240/api/flighttracker/double?number=2
        //[HttpGet("double")]
        //public IActionResult DoubleNumber([FromQuery] int number)
        //{
        //    int result = number * 2;
        //    Console.WriteLine($"!!! SERWER Otrzymano: {number}, Zwrocono: {result}");
        //    return Ok(new { original = number, doubled = result });
        //}



        [HttpGet("flightinfo")]
        public async Task<IActionResult> GetFlightData(string flightNumber)
        {

            Console.WriteLine($"!!! SERWER url: {flightNumber}");


            if (string.IsNullOrEmpty(flightNumber))
            {
                return BadRequest("Numer lotu nie mo¿e byæ pusty");
            }

            // tymczasowo zmienne key api
            // var apiKey = Environment.GetEnvironmentVariable("AVIATIONSTACK_API_KEY"); // Zmienna œrodowiskowa na klucz API
            //Console.WriteLine($"!!! SERWER api key: {apiKey}");

            // var url = $"http://api.aviationstack.com/v1/flights?access_key={apiKey}&flight_iata={flightNumber}";

            var url = $"http://api.aviationstack.com/v1/flights?access_key=47752a9dae134e0404fd13f4c0d48934&flight_iata={flightNumber}";


            //Console.WriteLine($"!!! SERWER api key: {apiKey}");
            Console.WriteLine($"!!! SERWER flightNumber: {flightNumber}");
            Console.WriteLine($"!!! SERWER url: {url}");

            try
            {
                // Wywo³anie zewnêtrznego API
                var response = await _httpClient.GetStringAsync(url);

                // Jeœli odpowiedŸ jest pusta
                if (string.IsNullOrEmpty(response))
                {
                    return NotFound("Nie znaleziono danych o locie.");
                }

                return Ok(response); // Zwróæ dane z zewnêtrznego API
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"B³¹d podczas ³¹czenia siê z API: {ex.Message}");
            }
        }
    }

    // Klasy odpowiedzi
    public class FlightApiResponse
    {
        public List<FlightData> data { get; set; }
    }

    public class FlightData
    {
        public Airline airline { get; set; }
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
        public string flight_status { get; set; }
    }

    public class Airline
    {
        public string name { get; set; }
        public string iata { get; set; }
    }

    public class Departure
    {
        public string airport { get; set; }
        public string iata { get; set; }
        public string scheduled { get; set; }
        public string timezone { get; set; }
        public int? delay { get; set; }
    }

    public class Arrival
    {
        public string airport { get; set; }
        public string iata { get; set; }
        public string scheduled { get; set; }
        public string timezone { get; set; }
        public string terminal { get; set; }
    }
}
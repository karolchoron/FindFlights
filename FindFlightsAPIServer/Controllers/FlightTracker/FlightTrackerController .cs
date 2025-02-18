using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FindFlightsAPI.Controllers
{
    [Route("api/flights")]
    [ApiController]
    public class FlightTrackerController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public FlightTrackerController(HttpClient httpClient, AviationStackSettings settings)
        {
            _httpClient = httpClient;

            _apiKey = settings.ApiKey;
            if (string.IsNullOrEmpty(_apiKey))
            {
                throw new InvalidOperationException("Brak API Key w konfiguracji!");
            }
        }

        [HttpGet("{flightNumber}")]
        public async Task<IActionResult> GetFlightData(string flightNumber)
        {
            if (string.IsNullOrEmpty(flightNumber))
            {
                return BadRequest("Numer lotu nie mo¿e byæ pusty");
            }

            var url = $"http://api.aviationstack.com/v1/flights?access_key={_apiKey}&flight_iata={flightNumber}";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    return HandleApiErrorResponse(response.StatusCode);
                }

                var flightData = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(flightData))
                {
                    return NotFound("Nie znaleziono danych o locie.");
                }

                return Ok(flightData); // Zwroc dane w formacie JSON
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"B³¹d podczas ³¹czenia siê z API: {ex.Message}");
            }
        }
        private IActionResult HandleApiErrorResponse(System.Net.HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case System.Net.HttpStatusCode.Unauthorized:
                    return Unauthorized();
                case System.Net.HttpStatusCode.Forbidden:
                    return Forbid();
                case System.Net.HttpStatusCode.NotFound:
                    return NotFound();
                case System.Net.HttpStatusCode.TooManyRequests:
                    return StatusCode(429);
                case System.Net.HttpStatusCode.InternalServerError:
                    return StatusCode(500);
                default:
                    return StatusCode(500);
            }
        }
    }
}
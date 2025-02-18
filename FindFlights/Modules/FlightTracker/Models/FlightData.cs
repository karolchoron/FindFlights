namespace FindFlights.Modules.FlightTracker.Models
{
    public class FlightData
    {
        public string flight_status { get; set; } // wymagana inna notacja niż appercase, aby prawidłowo zmapować odpowiedzi z API JSON na zmienne
        public Airline airline { get; set; }
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }
    }
}
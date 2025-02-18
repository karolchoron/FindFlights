namespace FindFlights.Modules.FlightTracker.Models
{
    public class Arrival
    {
        public string airport { get; set; }
        public string iata { get; set; }
        public string scheduled { get; set; }
        public string timezone { get; set; }
        public string terminal { get; set; }
        public int? delay { get; set; } // nullable int
    }
}
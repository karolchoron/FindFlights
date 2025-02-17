namespace FindFlights.Modules.FlightTracker
{
    public class Airport
    {
        public string airport { get; set; }
        public string iata { get; set; }
        public string scheduled { get; set; }
        public string terminal { get; set; }
        public int? delay { get; set; } // nullable int
        public string timezone { get; set; }
    }
}
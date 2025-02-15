namespace FindFlights.Modules.FlightTracker
{
    public class Airport
    {
        public string Name { get; set; }
        public string Iata { get; set; }
        public string Scheduled { get; set; }
        public string Terminal { get; set; }
        public int? Delay { get; set; }
    }

}
using System.Collections.Generic;

namespace Transavia.Application.Queries.GetAirports
{
    public class GetAirportsResult
    {
        public int TotalFound{ get; set; }

        public IEnumerable<Airport> Airports { get; set; }
    }

    public sealed class Airport
    {
        public string Iata { get; set; }

        public string Lon { get; set; }

        public string Lat { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public string Country { get; set; }

        public string Continent { get; set; }

        public string Type { get; set; }

        public string Size { get; set; }
    }
}
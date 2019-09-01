using System.Collections.Generic;

namespace Transavia.Web.MVC.Models
{
    public sealed class GetAirportsResult
    {
        public int TotalFound { get; set; }
        
        public IEnumerable<Airport> Airports { get; set; }
    }
}
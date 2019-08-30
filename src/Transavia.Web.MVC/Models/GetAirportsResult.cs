using System.Collections.Generic;

namespace Transavia.Web.MVC.Models
{
    public class GetAirportsResult
    {
        public int TotalFound { get; set; }
        
        public IEnumerable<Airport> Airports { get; set; }
    }
}
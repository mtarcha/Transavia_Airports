using System;

namespace Transavia.Application.Queries.GetSupportedAirportTypes
{
    public class AirportType
    {
        public Guid TypeId { get; set; }

        public string TypeName { get; set; }
    }
}
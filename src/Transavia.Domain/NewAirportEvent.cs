using System;
using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class NewAirportEvent : DomainEventBase
    {
        public NewAirportEvent(Guid airportId)
        {
            AirportId = airportId;
        }

        public Guid AirportId { get; }
    }
}
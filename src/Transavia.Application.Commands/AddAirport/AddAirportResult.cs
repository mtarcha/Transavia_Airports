using System;

namespace Transavia.Application.Commands.AddAirport
{
    public class AddAirportResult
    {
        public AddAirportResult(Guid createdAirportId)
        {
            CreatedAirportId = createdAirportId;
        }

        public Guid CreatedAirportId { get; }
    }
}
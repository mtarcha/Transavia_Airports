using System;

namespace Transavia.Application.Commands.AddAirport
{
    public sealed class AddAirportResult
    {
        public AddAirportResult(Guid createdAirportId)
        {
            CreatedAirportId = createdAirportId;
        }

        public Guid CreatedAirportId { get; }
    }
}
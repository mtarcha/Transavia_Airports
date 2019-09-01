using System;

namespace Transavia.Application.Queries.GetSupportedAirportStatuses
{
    public sealed class Status
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
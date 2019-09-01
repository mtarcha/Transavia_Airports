using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportStatuses
{
    public sealed class GetSupportedAirportStatusesQuery : IRequest<IEnumerable<Status>>
    {
    }
}
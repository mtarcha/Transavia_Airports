using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportStatuses
{
    public interface IGetSupportedAirportStatusesQueryHandler : IRequestHandler<GetSupportedAirportStatusesQuery, IEnumerable<Status>>
    {
    }
}
using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportStatuses
{
    public class GetSupportedAirportStatusesQuery : IRequest<IEnumerable<Status>>
    {
        
    }
}
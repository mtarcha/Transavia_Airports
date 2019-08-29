using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportTypes
{
    public interface IGetSupportedAirportTypesQueryHandler : IRequestHandler<GetSupportedAirportTypesQuery, IEnumerable<AirportType>>
    {
        
    }
}
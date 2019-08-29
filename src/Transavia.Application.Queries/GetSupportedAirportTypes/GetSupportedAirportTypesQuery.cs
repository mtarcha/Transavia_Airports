using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportTypes
{
    public class GetSupportedAirportTypesQuery : IRequest<IEnumerable<AirportType>>
    {
        
    }
}
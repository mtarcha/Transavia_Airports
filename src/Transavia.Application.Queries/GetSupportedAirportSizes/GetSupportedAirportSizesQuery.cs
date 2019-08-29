using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportSizes
{
    public class GetSupportedAirportSizesQuery : IRequest<IEnumerable<Size>>
    {
        
    }
}
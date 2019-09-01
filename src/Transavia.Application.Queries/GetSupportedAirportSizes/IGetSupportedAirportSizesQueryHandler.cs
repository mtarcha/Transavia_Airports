using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportSizes
{
    public interface IGetSupportedAirportSizesQueryHandler : IRequestHandler<GetSupportedAirportSizesQuery, IEnumerable<Size>>
    {
    }
}
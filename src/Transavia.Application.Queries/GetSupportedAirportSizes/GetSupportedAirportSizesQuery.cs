using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportSizes
{
    public sealed class GetSupportedAirportSizesQuery : IRequest<IEnumerable<Size>>
    {
    }
}
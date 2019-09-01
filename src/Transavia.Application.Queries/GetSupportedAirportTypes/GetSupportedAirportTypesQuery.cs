using System.Collections.Generic;
using MediatR;

namespace Transavia.Application.Queries.GetSupportedAirportTypes
{
    public sealed class GetSupportedAirportTypesQuery : IRequest<IEnumerable<AirportType>>
    {
    }
}
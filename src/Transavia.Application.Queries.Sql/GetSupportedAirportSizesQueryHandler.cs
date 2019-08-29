using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Application.Queries.GetSupportedAirportSizes;

namespace Transavia.Application.Queries.Sql
{
    public class GetSupportedAirportSizesQueryHandler : IGetSupportedAirportSizesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportSizesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public Task<IEnumerable<Size>> Handle(GetSupportedAirportSizesQuery request, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
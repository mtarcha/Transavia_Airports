using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Transavia.Application.Queries.GetSupportedAirportSizes;

namespace Transavia.Application.Queries.Sql
{
    public sealed class GetSupportedAirportSizesQueryHandler : IGetSupportedAirportSizesQueryHandler
    {
        private readonly IConnectionFactory _connectionFactory;

        public GetSupportedAirportSizesQueryHandler(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Size>> Handle(GetSupportedAirportSizesQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connectionFactory.Create())
            {
                return await connection.QueryAsync<Size>("select Id, SizeName as Name from dbo.Sizes");
            }
        }
    }
}
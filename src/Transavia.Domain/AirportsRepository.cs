using System;
using System.Threading;
using System.Threading.Tasks;
using Transavia.Domain.Common;

namespace Transavia.Domain
{
    public class AirportsRepository : IRepository<Airport, Guid>
    {
        public Task<Airport> CreateAsync(Airport entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Airport> GetByIdAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task<Airport> UpdateAsync(Airport entity, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id, CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
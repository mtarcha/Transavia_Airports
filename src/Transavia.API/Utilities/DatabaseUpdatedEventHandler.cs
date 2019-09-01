using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Transavia.Infrastructure.Cache;
using Transavia.Infrastructure.Data;

namespace Transavia.API.Utilities
{
    public sealed class DatabaseUpdatedEventHandler : INotificationHandler<DatabaseUpdatedEvent>
    {
        private readonly IDistributedCache _distributedCache;

        public DatabaseUpdatedEventHandler(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Handle(DatabaseUpdatedEvent notification, CancellationToken cancellationToken)
        {
            await _distributedCache.ClearCache(CancellationToken.None);
        }
    }
}
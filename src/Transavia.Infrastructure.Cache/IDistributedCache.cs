using System;
using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Infrastructure.Cache
{
    public interface IDistributedCache
    {
        Task<T> GetAsync<T>(string key, CancellationToken token = default);

        Task SetAsync<T>(string key, T value, TimeSpan absoluteExpiration, CancellationToken token = default);

        Task ClearCache(CancellationToken token = default);
    }
}
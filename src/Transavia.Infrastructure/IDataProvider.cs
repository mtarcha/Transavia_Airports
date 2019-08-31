using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Infrastructure
{
    public interface IDataProvider<TData>
    {
        Task<IEnumerable<TData>> GetData(CancellationToken token);
    }
}
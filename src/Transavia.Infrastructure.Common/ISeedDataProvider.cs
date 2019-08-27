using System.Collections.Generic;
using System.Threading.Tasks;

namespace Transavia.Infrastructure.Common
{
    public interface ISeedDataProvider<TData>
    {
        Task<IEnumerable<TData>> GetData();
    }
}
using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Infrastructure.Common
{
    public interface IStorageSeeder
    {
        Task SeedAsync(CancellationToken token);
    }
}
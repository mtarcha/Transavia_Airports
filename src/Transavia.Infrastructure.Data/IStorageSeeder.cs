using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Infrastructure.Data
{
    public interface IStorageSeeder
    {
        Task SeedAsync(CancellationToken token);
    }
}
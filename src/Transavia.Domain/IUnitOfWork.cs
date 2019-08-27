using System.Threading;
using System.Threading.Tasks;

namespace Transavia.Domain
{
    public interface IUnitOfWork
    {
        AirportsRepository Airports { get; }

        Task SaveChanges(CancellationToken token);
    }
}
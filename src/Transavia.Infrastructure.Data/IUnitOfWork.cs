using System.Threading;
using System.Threading.Tasks;
using Transavia.Infrastructure.Data.Repositories;

namespace Transavia.Infrastructure.Data
{
    public interface IUnitOfWork
    {
        IAirportsRepository Airports { get; }

        IAirportTypesRepository AirportTypes { get; }

        IStatusesRepository Statuses { get; }

        ISizesRepository Sizes { get; }

        ICountriesRepository Countries { get; }

        IContinentsRepository Continents { get; }
        
        Task SaveChanges(CancellationToken token);
    }
}
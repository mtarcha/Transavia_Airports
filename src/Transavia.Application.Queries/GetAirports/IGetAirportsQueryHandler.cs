using MediatR;

namespace Transavia.Application.Queries.GetAirports
{
    public interface IGetAirportsQueryHandler : IRequestHandler<GetAirportsQuery, GetAirportsResult>
    {
    }
}
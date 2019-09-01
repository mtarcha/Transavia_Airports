using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Transavia.Infrastructure.Data;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Application.Commands.AddAirport
{
    public sealed class AddAirportHandler : IRequestHandler<AddAirportCommand, AddAirportResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddAirportHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<AddAirportResult> Handle(AddAirportCommand request, CancellationToken cancellationToken)
        {
            var country = await _unitOfWork.Countries.GetByIdAsync(request.CountryId, cancellationToken);
            var airportType = await _unitOfWork.AirportTypes.GetByIdAsync(request.TypeId, cancellationToken);
            var size = await _unitOfWork.AirportSizes.GetByIdAsync(request.SizeId, cancellationToken);
            var status = await _unitOfWork.AirportStatuses.GetByIdAsync(request.StatusId, cancellationToken);

            var airport = new AirportEntity
            {
                Id = Guid.NewGuid(),
                Iata = request.Iata,
                Name = request.Name,
                Country = country,
                Type = airportType,
                Size = size,
                Status = status,
                Lon = request.Lon,
                Lat = request.Lat
            };

            await _unitOfWork.Airports.CreateAsync(airport, cancellationToken);
            await _unitOfWork.SaveChanges(cancellationToken);

            return new AddAirportResult(airport.Id);
        }
    }
}
using AutoMapper;
using Transavia.API.ViewModels;
using Transavia.Application.Commands.AddAirport;

namespace Transavia.API.Utilities
{
    public sealed class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AddAirportViewModel, AddAirportCommand>();
        }
    }
}
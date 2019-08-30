using AutoMapper;
using Transavia.Web.MVC.Models;
using Transavia.Web.MVC.ViewModels;

namespace Transavia.Web.MVC.Utilities
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<Country, CountryViewModel>();
        }
    }
}
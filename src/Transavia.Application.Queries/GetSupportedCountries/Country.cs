using System;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public class Country
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}
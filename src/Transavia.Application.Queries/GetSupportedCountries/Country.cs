using System;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public sealed class Country
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
    }
}
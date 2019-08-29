using System;

namespace Transavia.Application.Queries.GetSupportedCountries
{
    public class Country
    {
        public Guid Id { get; set; }

        public string Iso { get; set; }

        public Continent Continent { get; set; }
    }

    public class Continent
    {
        public Guid Id { get; set; }

        public string Code { get; set; }
    }
}
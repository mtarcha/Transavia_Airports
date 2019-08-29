using System;

namespace Transavia.Infrastructure.Data.Entities
{
    public class CountryEntity : Entity
    {
        public string Iso { get; set; }

        public string Name { get; set; }

        public ContinentEntity Continent { get; set; }
    }
}
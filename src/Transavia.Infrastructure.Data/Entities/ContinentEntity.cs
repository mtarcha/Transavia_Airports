using System;
using System.Collections.Generic;

namespace Transavia.Infrastructure.Data.Entities
{
    public class ContinentEntity : Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public IEnumerable<CountryEntity> Countries { get; set; }
    }
}
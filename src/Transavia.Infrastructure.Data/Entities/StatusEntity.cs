using System;

namespace Transavia.Infrastructure.Data.Entities
{
    public class StatusEntity : Entity
    {
        public int Code { get; set; }

        public string Name { get; set; }
    }
}
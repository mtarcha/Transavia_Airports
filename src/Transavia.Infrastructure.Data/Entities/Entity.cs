using System;

namespace Transavia.Infrastructure.Data.Entities
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
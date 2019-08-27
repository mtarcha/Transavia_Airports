using System;

namespace Transavia.Domain.Common
{
    public abstract class DomainEventBase : IDomainEvent
    {
        protected DomainEventBase()
        {
            RaiseTime = DateTime.Now.ToUniversalTime();
        }

        public DateTime RaiseTime { get; }
    }
}
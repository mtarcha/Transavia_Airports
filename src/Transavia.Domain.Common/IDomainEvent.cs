using System;
using MediatR;

namespace Transavia.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        DateTime RaiseTime { get; }
    }
}
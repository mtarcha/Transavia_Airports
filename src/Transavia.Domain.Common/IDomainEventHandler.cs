using MediatR;

namespace Transavia.Domain.Common
{
    public interface IDomainEventHandler<in T> : INotificationHandler<T> where T : IDomainEvent
    {
    }
}
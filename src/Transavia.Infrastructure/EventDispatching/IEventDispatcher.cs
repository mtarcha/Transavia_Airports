namespace Transavia.Infrastructure.EventDispatching
{
    public interface IEventDispatcher
    {
        void DispatchDeferred<T>(T domainEvent) where T : IEvent;

        void DispatchImmediately<T>(T domainEvent) where T : IEvent;

        void RaiseDeferredEvents();
    }
}
using System.Collections.Concurrent;
using MediatR;

namespace Transavia.Infrastructure.EventDispatching
{
    public sealed class EventDispatcher : IEventDispatcher
    {
        private readonly IMediator _mediator;       
        private readonly ConcurrentQueue<IEvent> _deferredEvents;

        public EventDispatcher(IMediator mediator)
        {
            _mediator = mediator;           
            _deferredEvents = new ConcurrentQueue<IEvent>();
        }

        public void DispatchDeferred<T>(T domainEvent) where T : IEvent
        {
            _deferredEvents.Enqueue(domainEvent);
        }

        public void DispatchImmediately<T>(T domainEvent) where T : IEvent
        {
            _mediator.Publish(domainEvent);
        }

        public void RaiseDeferredEvents()
        {
            while (_deferredEvents.TryDequeue(out var domainEvent))
            {
                _mediator.Publish(domainEvent);
            }
        }
    }
}
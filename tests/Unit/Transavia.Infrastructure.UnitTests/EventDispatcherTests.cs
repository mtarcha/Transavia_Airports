using System.Collections.Generic;
using MediatR;
using NSubstitute;
using NUnit.Framework;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.Infrastructure.UnitTests
{
    [TestFixture]
    public class EventDispatcherTests
    {
        private IMediator _mediator;
        private EventDispatcher _eventDispatcher;

        [SetUp]
        public void Setup()
        {
            _mediator = Substitute.For<IMediator>();
            _eventDispatcher = new EventDispatcher(_mediator);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(10)]
        public void DispatchDeferred_WithoutRaise_EventIsNotPublished(int eventsCount)
        {
            for (int i = 0; i < eventsCount; i++)
            {
                var notification = new TestNotification();

                _eventDispatcher.DispatchDeferred(notification);
            }

            _mediator.Received(0).Publish(Arg.Any<IEvent>());
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(15)]
        public void DispatchDeferred_RaiseDeferredEvents_EventArePublished(int eventsCount)
        {
            var notifications = new List<TestNotification>();
            for (int i = 0; i < eventsCount; i++)
            {
                var notification = new TestNotification();

                _eventDispatcher.DispatchDeferred(notification);

                notifications.Add(notification);
            }
          
            _eventDispatcher.RaiseDeferredEvents();

            foreach (var testNotification in notifications)
            {
                _mediator.Received(1).Publish(testNotification);
            }

            _mediator.Received(eventsCount).Publish(Arg.Any<IEvent>());
        }

        [Test]
        public void DispatchImmediately_EventIsRaisedImmediately()
        {
            var notification = new TestNotification();

            _eventDispatcher.DispatchImmediately(notification);

            _mediator.Received(1).Publish(notification);
        }

        private class TestNotification : IEvent
        {
        }
    }
}
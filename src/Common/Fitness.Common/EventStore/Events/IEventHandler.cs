using MediatR;

namespace Fitness.Common.EventStore.Events;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}
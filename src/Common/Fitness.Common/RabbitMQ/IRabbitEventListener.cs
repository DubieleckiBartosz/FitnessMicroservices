using Fitness.Common.EventStore.Events;

namespace Fitness.Common.RabbitMQ;

public interface IRabbitEventListener
{
    void Subscribe(Type type);
    void Subscribe<TEvent>() where TEvent : IEvent;
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    Task Publish(string message, string type);
}
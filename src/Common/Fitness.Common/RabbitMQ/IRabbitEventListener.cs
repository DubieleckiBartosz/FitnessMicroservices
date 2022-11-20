namespace Fitness.Common.RabbitMQ;

public interface IRabbitEventListener
{
    void Subscribe(Type type, string? queueName = null, string? routingKey = null);
    void Subscribe<TEvent>() where TEvent : IEvent;
    Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    void Publish(string message, string key);
}
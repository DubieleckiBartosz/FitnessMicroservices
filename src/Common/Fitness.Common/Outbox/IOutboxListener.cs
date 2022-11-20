namespace Fitness.Common.Outbox;

public interface IOutboxListener
{
    Task Commit(OutboxMessage message);
    Task Commit<TEvent>(TEvent @event) where TEvent : IEvent;
}
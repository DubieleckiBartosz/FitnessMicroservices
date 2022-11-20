namespace Fitness.Common.EventStore.Events;

public interface IEventBus
{
    Task PublishLocalAsync(params IEvent[] events);
    Task CommitAsync(params IEvent[] events);
    Task CommitStreamAsync(StreamState stream, string? key = null);
}
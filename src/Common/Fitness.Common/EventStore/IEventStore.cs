using Fitness.Common.EventStore.Aggregate;
using Fitness.Common.EventStore.Events;
using Fitness.Common.Projection;

namespace Fitness.Common.EventStore;

public interface IEventStore
{
    Task<TAggregate> AggregateStreamAsync<TAggregate>(Guid streamId, long? atStreamVersion = null,
        DateTime? atTimestamp = null) where TAggregate : AggregateRoot;

    Task<IReadOnlyList<StreamState>?> GetEventsAsync(Guid streamId, long? atStreamVersion = null,
        DateTime? atTimestamp = null);

    Task AppendEventAsync<TAggregate>(Guid streamId, IEvent @event, long? expectedVersion = null,
        Func<StreamState, Task>? action = null) where TAggregate : AggregateRoot;

    Task StoreAsync<TAggregate>(TAggregate aggregate, Func<StreamState, Task>? action = null)
        where TAggregate : AggregateRoot;
    void RegisterProjection(IProjection projection);

}
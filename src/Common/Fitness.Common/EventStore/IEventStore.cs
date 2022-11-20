namespace Fitness.Common.EventStore;

public interface IEventStore
{
    Task<TAggregate> AggregateStreamAsync<TAggregate>(Guid streamId, long? atStreamVersion = null,
        DateTime? atTimestamp = null) where TAggregate : Aggregate.Aggregate;

    Task<IReadOnlyList<StreamState>?> GetEventsAsync(Guid streamId, long? atStreamVersion = null,
        DateTime? atTimestamp = null);

    Task AppendEventAsync<TAggregate>(Guid streamId, IEvent @event, long? expectedVersion = null,
        Func<StreamState, string?, Task>? action = null) where TAggregate : Aggregate.Aggregate;

    Task StoreAsync<TAggregate>(TAggregate aggregate, Func<StreamState, string?, Task>? action = null)
        where TAggregate : Aggregate.Aggregate;
    void RegisterProjection(IProjection projection);

}
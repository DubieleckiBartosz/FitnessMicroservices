using Fitness.Common.EventStore.Events;
using Newtonsoft.Json;

namespace Fitness.Common.EventStore.Aggregate;

public abstract class AggregateRoot 
{
    public Guid Id { get; protected set; }
    public int Version { get; protected set; }
    public DateTime CreatedUtc { get; protected set; }

    [JsonIgnore]
    private readonly List<IEvent> _uncommittedEvents = new List<IEvent>();

    public IReadOnlyList<IEvent> DequeueUncommittedEvents()
    {
        var dequeuedEvents = _uncommittedEvents.AsReadOnly();

        _uncommittedEvents.Clear();

        return dequeuedEvents;
    }
    protected abstract void When(IEvent @event);
    public void Apply(IEvent @event)
    {
        Version++;

        When(@event);

        _uncommittedEvents.Add(@event);
    }  
}
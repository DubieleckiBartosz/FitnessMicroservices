namespace Fitness.Common.EventStore;

public class StreamState
{
    public StreamState(Guid aggregateId, string eventType, string streamType, string streamData)
    {
        Id = Guid.NewGuid();
        AggregateId = aggregateId;
        EventType = eventType;
        StreamType = streamType;
        StreamData = streamData;
        Version = 0; 
    }

    public Guid Id { get; private set; } 
    public Guid AggregateId { get; set; }
    public string EventType { get; set; }
    public string StreamType { get; set; }
    public string StreamData { get; set; }
    public int Version { get; set; } = 0;
}
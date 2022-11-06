namespace Fitness.Common.EventStore;

public class StreamState
{
    public StreamState(Guid streamId, string eventType, string streamType, string streamData)
    {
        Id = Guid.NewGuid();
        StreamId = streamId;
        EventType = eventType;
        StreamType = streamType;
        StreamData = streamData;
        Version = 0; 
    }

    internal StreamState()
    {
    }

    public Guid Id { get; private set; } 
    public Guid StreamId { get; set; }
    public string EventType { get; set; }
    public string StreamType { get; set; }
    public string StreamData { get; set; }
    public int Version { get; set; } = 0;
}
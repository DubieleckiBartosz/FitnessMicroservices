namespace Fitness.Common.EventStore;

[AttributeUsage(AttributeTargets.Class)]
public class EventQueueAttribute : Attribute
{
    //Routing or sth
    public string? QueueName { get; init; }

    public EventQueueAttribute()
    {
    }
    public EventQueueAttribute(string? queueName)
    {
        QueueName = queueName;
    }
}
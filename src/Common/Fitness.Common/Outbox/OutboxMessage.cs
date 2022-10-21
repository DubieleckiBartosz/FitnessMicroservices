using Fitness.Common.Types;

namespace Fitness.Common.Outbox;

public class OutboxMessage : IIdentifier
{
    internal OutboxMessage()
    {
    }

    public OutboxMessage(string type, string data)
    {
        Id = Guid.NewGuid();
        Type = type;
        Data = data;
        Created = DateTime.UtcNow;
    }

    public Guid Id { get; private set; }
    public DateTime Created { get; private set; }
    public string Type { get; set; }
    public string Data { get; set; }
    public DateTime? Processed { get; set; }
}
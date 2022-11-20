using Fitness.Common.Outbox;
using MediatR;

namespace Fitness.Common.EventStore.Events;

public class EventBus : IEventBus
{
    private readonly IMediator _mediator;
    private readonly IOutboxListener _outboxListener;

    public EventBus(IMediator mediator, IOutboxListener outboxListener)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _outboxListener = outboxListener ?? throw new ArgumentNullException(nameof(outboxListener));
    }
    public async Task PublishLocalAsync(params IEvent[] events)
    {
        foreach (var @event in events)
        {
            await _mediator.Publish(@event);
        }
    }

    public async Task CommitAsync(params IEvent[] events)
    {
        foreach (var @event in events)
        {
            await SendToMessageBroker(@event);
        }
    }

    public async Task CommitStreamAsync(StreamState stream, string? key = null)
    { 
        var message = new OutboxMessage(stream.EventType, stream.StreamData,
            key ?? CreateAlternativeKey(stream.EventType));
        await _outboxListener.Commit(message);
    }

    private async Task SendToMessageBroker(IEvent @event)
    {
        await _outboxListener.Commit(@event);
    }

    private string CreateAlternativeKey(string typeName)
    {
        var indexTypeName = typeName.IndexOf(',');
        var baseTypeName = typeName.Substring(0, indexTypeName) + "_key";
        var indexKey = baseTypeName.LastIndexOf('.');
        var key = baseTypeName.Substring(indexKey + 1);

        var builder = new StringBuilder();

        foreach (var charItem in key)
        {
            if (char.IsUpper(charItem))
            {
                if (builder.Length > 0)
                {
                    builder.Append('_');
                }

                builder.Append(char.ToLower(charItem));
            }
            else
            {
                builder.Append(charItem);
            }
        }

        var responseKey = builder.ToString();

        return responseKey;
    }
}
 
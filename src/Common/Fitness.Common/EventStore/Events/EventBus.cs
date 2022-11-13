using Fitness.Common.Core;
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

    public async Task CommitStreamAsync(StreamState stream)
    {
        // Mapping.Map<StreamState, OutboxMessage>(stream);
        var message = new OutboxMessage(stream.EventType, stream.StreamData);
        await _outboxListener.Commit(message);
    }

    private async Task SendToMessageBroker(IEvent @event)
    {
        await _outboxListener.Commit(@event);
    }
}
 
using Fitness.Common.EventStore.Events;

namespace Fitness.Common.Projection;

public interface IProjection
{
    Type[] Handles { get; }
    Task Handle(IEvent @event, CancellationToken cancellationToken);
}
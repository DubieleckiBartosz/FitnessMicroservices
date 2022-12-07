using Enrollment.Application.Constants;
using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingStartEnrollments;

[EventQueue(routingKey: Keys.TrainingKeyQueueRoutingKey)]
public record TrainingPublished(Guid TrainingId, Guid Creator) : IEvent
{
    public static TrainingPublished Create(Guid trainingId, Guid creator)
    {
        return new TrainingPublished(trainingId, creator);
    }
}
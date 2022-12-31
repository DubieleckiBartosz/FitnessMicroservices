using Enrollment.Application.Constants;
using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingStartEnrollments;

[EventQueue(routingKey: Keys.ShareTrainingQueueRoutingKey)]
public record TrainingPublished(Guid TrainingId, Guid TrainerCode) : IEvent
{
    public static TrainingPublished Create(Guid trainingId, Guid trainerCode)
    {
        return new TrainingPublished(trainingId, trainerCode);
    }
}
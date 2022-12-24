using Enrollment.Application.Constants;
using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingCloseEnrollment;

[EventQueue(routingKey: Keys.CloseTrainingQueueRoutingKey)]

public record TrainingClosed(Guid EnrollmentId, Guid MarkedBy) : IEvent
{
    public static TrainingClosed Create(Guid enrollmentId, Guid markedBy)
    {
        return new TrainingClosed(enrollmentId, markedBy);
    }
}
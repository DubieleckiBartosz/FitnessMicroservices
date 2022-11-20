using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events; 

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

[EventQueue(routingKey: "training_shared_key")]
public record TrainingEnrollmentsStarted(Guid TrainingId) : IEvent 
{
    public static TrainingEnrollmentsStarted Create(Guid trainingId)
    {
        return new TrainingEnrollmentsStarted(trainingId);
    }
}
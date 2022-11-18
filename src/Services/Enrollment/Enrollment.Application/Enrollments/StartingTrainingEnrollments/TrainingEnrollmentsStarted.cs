using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events; 

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

[EventQueue]
public record TrainingEnrollmentsStarted(Guid TrainingId) : IEvent 
{
    public static TrainingEnrollmentsStarted Create(Guid trainingId)
    {
        return new TrainingEnrollmentsStarted(trainingId);
    }
}
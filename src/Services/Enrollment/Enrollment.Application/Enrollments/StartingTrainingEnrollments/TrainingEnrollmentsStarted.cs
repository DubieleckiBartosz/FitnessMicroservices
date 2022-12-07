using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

public record TrainingEnrollmentsStarted(Guid EnrollmentId, Guid TrainingId, Guid Creator) : IEvent 
{
    public static TrainingEnrollmentsStarted Create(Guid enrollmentId, Guid trainingId, Guid creator)
    { 
        return new TrainingEnrollmentsStarted(enrollmentId, trainingId, creator);
    }
}
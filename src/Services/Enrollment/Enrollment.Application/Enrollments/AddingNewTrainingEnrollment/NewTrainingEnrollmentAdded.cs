using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;

public record NewTrainingEnrollmentAdded(Guid UserId, Guid TrainingId) : IEvent
{
    public static NewTrainingEnrollmentAdded Create(Guid userId, Guid trainingId)
    {
        return new NewTrainingEnrollmentAdded(userId, trainingId);
    }
}
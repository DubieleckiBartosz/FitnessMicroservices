using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.AddingNewTrainingEnrollment;

public record NewTrainingEnrollmentAdded(Guid EnrollmentId, int UserId, Guid TrainingId) : IEvent
{
    public static NewTrainingEnrollmentAdded Create(Guid enrollmentId, int userId, Guid trainingId)
    {
        return new NewTrainingEnrollmentAdded(enrollmentId, userId, trainingId);
    }
}
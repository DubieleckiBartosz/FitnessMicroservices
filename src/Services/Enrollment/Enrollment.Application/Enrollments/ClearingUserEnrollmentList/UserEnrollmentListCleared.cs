using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.ClearingUserEnrollmentList;

public record UserEnrollmentListCleared(Guid EnrollmentId) : IEvent
{
    public static UserEnrollmentListCleared Create(Guid enrollmentId)
    {
        return new UserEnrollmentListCleared(enrollmentId);
    }
}
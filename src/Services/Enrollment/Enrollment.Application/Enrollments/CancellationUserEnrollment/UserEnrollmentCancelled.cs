using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.CancellationUserEnrollment;

public record UserEnrollmentCancelled (Guid UserEnrollmentId, Guid CancelBy) : IEvent
{
    public static UserEnrollmentCancelled Create(Guid userEnrollmentId, Guid cancelBy)
    {
        return new UserEnrollmentCancelled(userEnrollmentId, cancelBy);
    }
}
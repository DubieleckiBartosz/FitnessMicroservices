using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.CancellationUserEnrollment;

public record UserEnrollmentCancelled (Guid UserEnrollmentId, Guid CancelByCreator, int CancelByUser) : IEvent
{
    public static UserEnrollmentCancelled Create(Guid userEnrollmentId, Guid cancelByCreator, int cancelByUser)
    {
        return new UserEnrollmentCancelled(userEnrollmentId, cancelByCreator, cancelByUser);
    }
}
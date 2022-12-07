using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.AcceptingUserEnrollment;

public record UserEnrollmentAccepted(Guid UserEnrollmentId, Guid AcceptBy) : IEvent
{
    public static UserEnrollmentAccepted Create(Guid userEnrollmentId, Guid acceptBy)
    {
        return new UserEnrollmentAccepted(userEnrollmentId, acceptBy);
    }
}
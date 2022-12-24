using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.OpeningEnrollment;

public record EnrollmentOpened(Guid EnrollmentId) : IEvent
{
    public static EnrollmentOpened Create(Guid enrollmentId)
    {
        return new EnrollmentOpened(enrollmentId);
    }
}
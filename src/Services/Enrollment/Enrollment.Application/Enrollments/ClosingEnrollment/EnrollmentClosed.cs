using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.ClosingEnrollment;

public record EnrollmentClosed(Guid EnrollmentId) : IEvent
{
    public static EnrollmentClosed Create(Guid enrollmentId)
    {
        return new EnrollmentClosed(enrollmentId);
    }
}
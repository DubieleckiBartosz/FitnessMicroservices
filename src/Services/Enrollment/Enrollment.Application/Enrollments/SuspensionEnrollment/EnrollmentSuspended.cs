using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.SuspensionEnrollment;

public record EnrollmentSuspended(Guid EnrollmentId) : IEvent 
{
    public static EnrollmentSuspended Create(Guid enrollmentId)
    {
        return new EnrollmentSuspended(enrollmentId);
    }
}
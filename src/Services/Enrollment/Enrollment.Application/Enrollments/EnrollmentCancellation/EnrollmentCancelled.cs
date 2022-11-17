using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.EnrollmentCancellation;

public record EnrollmentCancelled() : IEvent
{
}
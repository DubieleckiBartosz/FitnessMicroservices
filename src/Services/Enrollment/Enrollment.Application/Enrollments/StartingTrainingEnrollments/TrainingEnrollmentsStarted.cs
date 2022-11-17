using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Enrollments.StartingTrainingEnrollments;

public record TrainingEnrollmentsStarted() : IEvent
{
}
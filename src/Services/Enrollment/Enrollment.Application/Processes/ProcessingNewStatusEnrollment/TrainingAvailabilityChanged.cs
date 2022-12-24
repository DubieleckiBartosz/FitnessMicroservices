using Enrollment.Application.Processes.TrainingEnums;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingNewStatusEnrollment;
 
public record TrainingAvailabilityChanged(Guid EnrollmentId, TrainingAvailability NewAvailability, Guid ChangedBy) : IEvent;
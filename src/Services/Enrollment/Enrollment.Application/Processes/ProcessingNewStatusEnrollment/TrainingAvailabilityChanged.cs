using Enrollment.Application.Constants;
using Enrollment.Application.Processes.TrainingEnums;
using Fitness.Common.EventStore;
using Fitness.Common.EventStore.Events;

namespace Enrollment.Application.Processes.ProcessingNewStatusEnrollment;

[EventQueue(routingKey: Keys.AvailabilityChangedQueueRoutingKey)]
public record TrainingAvailabilityChanged(Guid EnrollmentId, TrainingAvailability NewAvailability, Guid ChangedBy) : IEvent;
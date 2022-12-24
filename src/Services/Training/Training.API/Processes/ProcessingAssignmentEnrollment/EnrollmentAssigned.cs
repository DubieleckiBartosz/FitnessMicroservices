using Fitness.Common.EventStore;

namespace Training.API.Processes.ProcessingAssignmentEnrollment;

[EventQueue(routingKey: Keys.StartEnrollmentQueueRoutingKey)]

public record EnrollmentAssigned(Guid TrainingId, Guid EnrollmentId) : IEvent;

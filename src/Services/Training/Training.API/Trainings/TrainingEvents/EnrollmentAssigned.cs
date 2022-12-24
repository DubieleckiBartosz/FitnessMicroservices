namespace Training.API.Trainings.TrainingEvents;

public record EnrollmentAssigned(Guid TrainingId, Guid EnrollmentId) : IEvent
{
    public static EnrollmentAssigned Create(Guid trainingId, Guid enrollmentId)
    {
        return new EnrollmentAssigned(trainingId, enrollmentId);
    }
}
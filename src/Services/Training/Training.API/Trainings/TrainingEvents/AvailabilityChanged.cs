namespace Training.API.Trainings.TrainingEvents;

public record AvailabilityChanged(Guid TrainingId, Guid? EnrollmentId, TrainingAvailability NewAvailability, Guid ChangedBy) : IEvent
{
    public static AvailabilityChanged Create(Guid trainingId, Guid? enrollmentId, TrainingAvailability newAvailability, Guid changedBy)
    {
        return new AvailabilityChanged(trainingId, enrollmentId, newAvailability, changedBy);
    }
}
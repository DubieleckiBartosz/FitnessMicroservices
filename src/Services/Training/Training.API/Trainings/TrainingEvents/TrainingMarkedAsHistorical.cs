namespace Training.API.Trainings.TrainingEvents;

public record TrainingMarkedAsHistorical(Guid TrainingId, Guid? EnrollmentId, Guid MarkedBy) : IEvent
{
    public static TrainingMarkedAsHistorical Create(Guid trainingId, Guid? enrollmentId, Guid markedBy)
    {
        return new TrainingMarkedAsHistorical(trainingId, enrollmentId, markedBy);
    }
}
namespace Training.API.Trainings.TrainingEvents;

public record TrainingShared(Guid TrainingId) : IEvent
{
    public static TrainingShared Create(Guid trainingId)
    {
        return new TrainingShared(trainingId);
    }
}
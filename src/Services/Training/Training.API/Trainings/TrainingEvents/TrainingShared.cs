namespace Training.API.Trainings.TrainingEvents;

public record TrainingShared(Guid TrainingId) : IEvent
{
    public static TrainingShared Create(Guid TrainingId)
    {
        return new TrainingShared(TrainingId);
    }
}
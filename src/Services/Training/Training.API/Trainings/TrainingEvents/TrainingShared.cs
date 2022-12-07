namespace Training.API.Trainings.TrainingEvents;

public record TrainingShared(Guid TrainingId, Guid TrainerCode) : IEvent
{
    public static TrainingShared Create(Guid trainingId, Guid trainerCode)
    {
        return new TrainingShared(trainingId, trainerCode);
    }
}
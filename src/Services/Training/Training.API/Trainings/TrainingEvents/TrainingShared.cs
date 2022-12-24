namespace Training.API.Trainings.TrainingEvents;

public record TrainingShared(Guid TrainingId, Guid TrainerCode, bool IsPublic) : IEvent
{
    public static TrainingShared Create(Guid trainingId, Guid trainerCode, bool isPublic)
    {
        return new TrainingShared(trainingId, trainerCode, isPublic);
    }
}
namespace Training.API.Trainings.TrainingEvents;

public record NewTrainingInitiated(Guid TrainerUniqueCode, Guid TrainingId, DateTime Created) : IEvent
{
    public static NewTrainingInitiated Create(Guid trainerUniqueCode, Guid trainingId, DateTime created)
    {
        return new NewTrainingInitiated(trainerUniqueCode, trainingId, created);
    }
}
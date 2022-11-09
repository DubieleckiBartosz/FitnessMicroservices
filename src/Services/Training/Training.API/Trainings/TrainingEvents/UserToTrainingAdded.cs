namespace Training.API.Trainings.TrainingEvents;

public record UserToTrainingAdded(Guid UserId, Guid TrainingId) : IEvent
{
    public static UserToTrainingAdded Create(Guid userId, Guid trainingId)
    {
        return new UserToTrainingAdded(userId, trainingId);
    }
}
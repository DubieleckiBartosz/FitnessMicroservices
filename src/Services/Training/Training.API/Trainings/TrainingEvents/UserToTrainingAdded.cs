namespace Training.API.Trainings.TrainingEvents
{
    public record UserToTrainingAdded(Guid UserId, Guid TrainingId) : IEvent
    {
        public static UserToTrainingAdded Create(Guid userId, Guid TrainingId)
        {
            return new UserToTrainingAdded(userId, TrainingId);
        }
    }
}

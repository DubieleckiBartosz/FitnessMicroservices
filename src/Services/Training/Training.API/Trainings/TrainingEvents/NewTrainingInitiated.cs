using Fitness.Common.EventStore.Events;

namespace Training.API.Trainings.TrainingEvents
{
    public record NewTrainingInitiated(Guid CreatorId, Guid TrainingId, DateTime Created) : IEvent
    {
        public static NewTrainingInitiated Create(Guid creatorId, Guid trainingId, DateTime created)
        {
            return new NewTrainingInitiated(creatorId, trainingId, created);
        }
    }
}

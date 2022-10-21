using Fitness.Common.EventStore.Events;

namespace Training.API.Trainings.TrainingEvents
{
    public record NewTrainingInitiated(Guid TrainerId, Guid TrainingId, DateTime Created) : IEvent
    {
        public static NewTrainingInitiated Create(Guid trainerId, Guid trainingId, DateTime created)
        {
            return new NewTrainingInitiated(trainerId, trainingId, created);
        }
    }
}

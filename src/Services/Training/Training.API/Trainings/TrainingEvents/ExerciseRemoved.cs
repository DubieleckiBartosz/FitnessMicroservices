using Fitness.Common.EventStore.Events;

namespace Training.API.Trainings.TrainingEvents
{
    public record ExerciseRemoved(Guid ExerciseId, Guid TrainingId) : IEvent
    {
        public static ExerciseRemoved Create(Guid exerciseId, Guid trainingId)
        {
            return new ExerciseRemoved(exerciseId, trainingId);
        }
    }
}

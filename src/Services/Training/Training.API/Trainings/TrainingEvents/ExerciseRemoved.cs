using Fitness.Common.EventStore.Events;

namespace Training.API.Trainings.TrainingEvents
{
    public record ExerciseRemoved(Guid ExerciseId) : IEvent
    {
        public static ExerciseRemoved Create(Guid exerciseId)
        {
            return new ExerciseRemoved(exerciseId);
        }
    }
}

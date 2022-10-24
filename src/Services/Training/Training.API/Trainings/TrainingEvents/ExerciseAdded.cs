using Fitness.Common.EventStore.Events;

namespace Training.API.Trainings.TrainingEvents
{
    public record ExerciseAdded(TrainingExercise Training) : IEvent
    {
        public static ExerciseAdded Create(TrainingExercise training)
        {
            return new ExerciseAdded(training);
        }
    }
}

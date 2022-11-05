using Fitness.Common.EventStore.Events;
using Training.API.Trainings.ReadModels;

namespace Training.API.Trainings.TrainingEvents
{
    public record ExerciseAdded(Guid TrainingId,TrainingExercise Training) : IEvent
    {
        public static ExerciseAdded Create(TrainingExercise training, Guid trainingId)
        {
            return new ExerciseAdded(trainingId, training);
        }
    }
}

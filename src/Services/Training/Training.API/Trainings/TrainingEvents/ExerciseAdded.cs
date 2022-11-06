namespace Training.API.Trainings.TrainingEvents;

public record ExerciseAdded(Guid TrainingId, TrainingExercise Exercise) : IEvent
{
    public static ExerciseAdded Create(TrainingExercise exercise, Guid trainingId)
    {
        return new ExerciseAdded(trainingId, exercise);
    }
}
namespace Training.API.Trainings.TrainingEvents;

public record ExerciseRemoved(Guid ExerciseId, Guid TrainingId, int NumberRepetitions) : IEvent
{
    public static ExerciseRemoved Create(Guid exerciseId, Guid trainingId, int NumberRepetitions)
    {
        return new ExerciseRemoved(exerciseId, trainingId, NumberRepetitions);
    }
}
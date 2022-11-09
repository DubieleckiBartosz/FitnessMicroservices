namespace Training.API.Trainings.TrainingEvents;

public record ExerciseRemoved(Guid ExerciseId, Guid TrainingId, int NumberRepetitions) : IEvent
{
    public static ExerciseRemoved Create(Guid exerciseId, Guid trainingId, int numberRepetitions)
    {
        return new ExerciseRemoved(exerciseId, trainingId, numberRepetitions);
    }
}
namespace Training.API.Trainings;

public class TrainingExercise
{
    public Guid Id { get; private set; }
    public Guid ExternalExerciseId { get; private set; }
    public int NumberRepetitions { get; private set; }
    public int BreakBetweenSetsInMinutes { get; private set; }
    public Guid TrainingId { get; private set; }
    public Training Training { get; private set; }

    private TrainingExercise(Guid externalExerciseId, int numberRepetitions, int breakBetweenSetsInMinutes,
        Training training)
    {
        ExternalExerciseId = externalExerciseId;
        NumberRepetitions = numberRepetitions;
        BreakBetweenSetsInMinutes = breakBetweenSetsInMinutes;
        Training = training;
        Id = Guid.NewGuid();
    }

    public static TrainingExercise CreateExercise(Guid externalExerciseId, int numberRepetitions, int breakBetweenSetsInMinutes,
        Training training)
    {
        return new TrainingExercise(externalExerciseId, numberRepetitions, breakBetweenSetsInMinutes, training);
    }
}
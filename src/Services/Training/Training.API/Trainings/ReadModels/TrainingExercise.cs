using Fitness.Common.Projection;

namespace Training.API.Trainings.ReadModels;

public class TrainingExercise : IRead
{
    public Guid Id { get; private set; }
    public bool IsDeleted { get; set; }
    public Guid ExternalExerciseId { get; private set; }
    public int NumberRepetitions { get; private set; }
    public int BreakBetweenSetsInMinutes { get; private set; }

    internal TrainingExercise()
    {
    }

    private TrainingExercise(Guid externalExerciseId, int numberRepetitions, int breakBetweenSetsInMinutes)
    {
        ExternalExerciseId = externalExerciseId;
        NumberRepetitions = numberRepetitions;
        BreakBetweenSetsInMinutes = breakBetweenSetsInMinutes;
        Id = Guid.NewGuid();
    }

    public static TrainingExercise CreateExercise(Guid externalExerciseId, int numberRepetitions,
        int breakBetweenSetsInMinutes)
    {
        return new TrainingExercise(externalExerciseId, numberRepetitions, breakBetweenSetsInMinutes);
    }

    public TrainingExercise Update(int? numberRepetitions, int? breakBetweenSetsInMinutes)
    {
        NumberRepetitions = numberRepetitions ?? NumberRepetitions;
        BreakBetweenSetsInMinutes = breakBetweenSetsInMinutes ?? BreakBetweenSetsInMinutes;

        return this;
    }
}
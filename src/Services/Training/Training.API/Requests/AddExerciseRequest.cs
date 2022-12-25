using Newtonsoft.Json;

namespace Training.API.Requests;

public class AddExerciseRequest
{ 
    public Guid ExternalExerciseId { get; init; }
    public int NumberRepetitions { get; init; }
    public int BreakBetweenSetsInMinutes { get; init; }
    public Guid TrainingId { get; init; }

    public AddExerciseRequest()
    {
    }

    [JsonConstructor]
    public AddExerciseRequest(Guid externalExerciseId, int numberRepetitions, int breakBetweenSetsInMinutes, Guid trainingId)
    {
        ExternalExerciseId = externalExerciseId;
        NumberRepetitions = numberRepetitions;
        BreakBetweenSetsInMinutes = breakBetweenSetsInMinutes;
        TrainingId = trainingId;
    }
}
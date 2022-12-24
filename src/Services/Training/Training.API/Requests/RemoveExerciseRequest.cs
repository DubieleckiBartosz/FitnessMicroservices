using Newtonsoft.Json;

namespace Training.API.Requests;

public class RemoveExerciseRequest
{ 
    public Guid TrainingId { get; init; }
    public Guid ExerciseId { get; init; }
    public int NumberRepetitions { get; init; }
    public RemoveExerciseRequest()
    {
    }

    [JsonConstructor]
    public RemoveExerciseRequest(Guid trainingId, Guid exerciseId, int numberRepetitions)
    {
        TrainingId = trainingId;
        ExerciseId = exerciseId;
        NumberRepetitions = numberRepetitions;
    }
}
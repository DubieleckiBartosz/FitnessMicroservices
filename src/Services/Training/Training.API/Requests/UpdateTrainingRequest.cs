using Newtonsoft.Json;

namespace Training.API.Requests;

public class UpdateTrainingRequest
{ 
    public Guid TrainingId { get; init; }
    public int? DurationTrainingInMinutes { get; init; }
    public int? BreakBetweenExercisesInMinutes { get; init; }
    public decimal? Price { get; init; }

    public UpdateTrainingRequest()
    {
    }
    [JsonConstructor]
    public UpdateTrainingRequest(Guid trainingId, int? durationTrainingInMinutes, int? breakBetweenExercisesInMinutes, decimal? price)
    {
        TrainingId = trainingId;
        DurationTrainingInMinutes = durationTrainingInMinutes;
        BreakBetweenExercisesInMinutes = breakBetweenExercisesInMinutes;
        Price = price;
    }
}
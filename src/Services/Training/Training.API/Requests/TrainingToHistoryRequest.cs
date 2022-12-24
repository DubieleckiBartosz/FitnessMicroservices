using Newtonsoft.Json;

namespace Training.API.Requests;

public class TrainingToHistoryRequest
{
    public Guid TrainingId { get; init; }
    [JsonConstructor]
    public TrainingToHistoryRequest(Guid trainingId)
    {
        TrainingId = trainingId;
    }
}
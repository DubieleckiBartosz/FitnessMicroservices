using Newtonsoft.Json;

namespace Training.API.Requests;

public class ShareTrainingRequest
{ 
    public Guid TrainingId { get; init; }
    public bool IsPublic { get; init; }

    public ShareTrainingRequest()
    {
    }

    [JsonConstructor]
    public ShareTrainingRequest(Guid trainingId, bool isPublic)
    {
        TrainingId = trainingId;
        IsPublic = isPublic;
    }
}
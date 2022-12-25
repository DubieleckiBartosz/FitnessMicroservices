using Newtonsoft.Json;

namespace Training.API.Requests;

public class AddUserToTrainingRequest
{ 
    public Guid UserId { get; init; }
    public Guid TrainingId { get; init; }

    public AddUserToTrainingRequest()
    {
    }

    [JsonConstructor]
    public AddUserToTrainingRequest(Guid userId, Guid trainingId)
    {
        UserId = userId;
        TrainingId = trainingId;
    }
}
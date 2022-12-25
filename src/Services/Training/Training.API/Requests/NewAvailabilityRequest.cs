using Newtonsoft.Json;

namespace Training.API.Requests;

public class NewAvailabilityRequest
{ 
    public Guid TrainingId { get; init; }
    public TrainingAvailability NewAvailability { get; init; }

    public NewAvailabilityRequest()
    {
    }

    [JsonConstructor]
    public NewAvailabilityRequest(Guid trainingId, TrainingAvailability newAvailability)
    {
        TrainingId = trainingId;
        NewAvailability = newAvailability;
    }
}
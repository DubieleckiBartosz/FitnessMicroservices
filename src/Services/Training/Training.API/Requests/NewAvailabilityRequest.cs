namespace Training.API.Requests;

public record NewAvailabilityRequest(Guid TrainingId, TrainingAvailability NewAvailability);
namespace Training.API.Requests;

public record AddUserToTrainingRequest(Guid UserId, Guid TrainingId);
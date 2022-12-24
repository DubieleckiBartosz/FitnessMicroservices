namespace Training.API.Requests;

public record ShareTrainingRequest(Guid TrainingId, bool IsPublic);
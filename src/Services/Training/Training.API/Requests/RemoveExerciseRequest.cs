namespace Training.API.Requests;

public record RemoveExerciseRequest(Guid TrainingId, Guid ExerciseId, int NumberRepetitions);
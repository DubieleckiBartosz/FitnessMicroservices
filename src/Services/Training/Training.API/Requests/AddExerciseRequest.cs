namespace Training.API.Requests
{
    public record AddExerciseRequest(Guid ExternalExerciseId, int NumberRepetitions, int BreakBetweenSetsInMinutes,
        Guid TrainingId);
}

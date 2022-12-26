namespace Training.API.Trainings.TrainingEvents;

public record TrainingDataUpdated(Guid TrainingId, int? DurationTrainingInMinutes,
    int? BreakBetweenExercisesInMinutes,
    decimal? Price) : IEvent
{
    public static TrainingDataUpdated Create(Guid trainingId, int? durationTrainingInMinutes,
        int? breakBetweenExercisesInMinutes,
        decimal? price)
    {
        return new TrainingDataUpdated(trainingId, durationTrainingInMinutes, breakBetweenExercisesInMinutes,
            price);
    }
}
namespace Training.API.Handlers.ViewModels;

public record TrainingDetailsViewModel
{
    public Guid Id { get; init; }
    public bool IsActive { get; init; }
    public int NumberUsersEnrolled { get; init; }
    public decimal? Price { get; init; }
    public TrainingStatus Status { get; init; }
    public TrainingAvailability Availability { get; init; }
    public TrainingType? Type { get; init; }
    public int? DurationTrainingInMinutes { get; init; }
    public int? BreakBetweenExercisesInMinutes { get; init; }
    public DateTime Created { get; init; }
    public Guid TrainerUniqueCode { get; init; }
    public List<ExerciseViewModel>? TrainingExercises { get; init; }
}
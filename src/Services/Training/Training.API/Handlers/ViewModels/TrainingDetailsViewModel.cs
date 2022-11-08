namespace Training.API.Handlers.ViewModels;

public record TrainingDetailsViewModel(Guid Id, Guid TrainerUniqueCode, List<ExerciseViewModel> TrainingExercises,
    bool IsActive, decimal? Price, TrainingAvailability Availability, TrainingType? Type,
    int? DurationTrainingInMinutes, int? BreakBetweenExercisesInMinutes);
namespace Training.API.Handlers.ViewModels;

public record ExerciseViewModel
{
    public Guid Id { get; init; }  
    public int NumberRepetitions { get; init; }
    public int BreakBetweenSetsInMinutes { get; init; }
}
namespace Exercise.Application.Features.Views;

public record GetExerciseByIdViewModel(Guid Id, string Name, Guid CreatedBy, string Video, string Description, List<ImageViewModel> Images);
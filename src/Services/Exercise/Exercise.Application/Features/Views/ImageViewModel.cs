namespace Exercise.Application.Features.Views;

public record ImageViewModel(Guid Id, string ImagePath, string ImageTitle, bool IsMain , string? Description);
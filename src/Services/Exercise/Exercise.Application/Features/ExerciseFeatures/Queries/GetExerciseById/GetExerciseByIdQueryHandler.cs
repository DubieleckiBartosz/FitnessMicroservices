using Exercise.Application.Contracts;
using Exercise.Application.Features.Views;
using Fitness.Common.Abstractions;
using Fitness.Common.Core.Exceptions;

namespace Exercise.Application.Features.ExerciseFeatures.Queries.GetExerciseById;

public class GetExerciseByIdQueryHandler : IQueryHandler<GetExerciseByIdQuery, GetExerciseByIdViewModel>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseByIdQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public async Task<GetExerciseByIdViewModel> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        var exercise = await _exerciseRepository.GetByIdAsync(request.ExerciseId);

        if (exercise == null)
        {
            throw new NotFoundException("Exercise cannot be null.", "Exercise not found");
        }

        var images = exercise.Images.Select(_ =>
            new ImageViewModel(_.Id, _.ImagePath, _.ImageTitle, _.IsMain, _.Description?.Description)).ToList();

        return new GetExerciseByIdViewModel(exercise.Id, exercise.Name, exercise.CreatedBy, exercise.Video?.VideoLink,
            exercise.Description.Description, images);
    }
}
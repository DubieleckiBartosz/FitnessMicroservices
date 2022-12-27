using Exercise.Application.Contracts;
using Exercise.Application.Features.Views;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Queries.GetExerciseById;

public class GetExerciseByIdQueryHandler : IQueryHandler<GetExerciseByIdQuery, GetExerciseByIdViewModel>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExerciseByIdQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public Task<GetExerciseByIdViewModel> Handle(GetExerciseByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
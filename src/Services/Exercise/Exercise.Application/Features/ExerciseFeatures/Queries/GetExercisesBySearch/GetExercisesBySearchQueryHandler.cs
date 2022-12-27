using Exercise.Application.Contracts;
using Exercise.Application.Features.Views;
using Fitness.Common.Abstractions;

namespace Exercise.Application.Features.ExerciseFeatures.Queries.GetExercisesBySearch;

public class GetExercisesBySearchQueryHandler : IQueryHandler<GetExercisesBySearchQuery, List<GetExerciseBySearchViewModel>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public GetExercisesBySearchQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository ?? throw new ArgumentNullException(nameof(exerciseRepository));
    }

    public Task<List<GetExerciseBySearchViewModel>> Handle(GetExercisesBySearchQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
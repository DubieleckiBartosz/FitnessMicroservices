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

    public async Task<List<GetExerciseBySearchViewModel>> Handle(GetExercisesBySearchQuery request, CancellationToken cancellationToken)
    {
        var resultSearch = await _exerciseRepository.GetBySearchAsync(request.Id, request.Name, request.Sort.Type,
            request.Sort.Name, request.SearchQuery.PageNumber, request.SearchQuery.PageSize);

        var resultMap = resultSearch?.Select(_ =>
            new GetExerciseBySearchViewModel(_.Id, _.Name, _.CreatedBy, _.Video?.VideoLink, _.Description.Description)).ToList();

        return resultMap ?? new List<GetExerciseBySearchViewModel>();
    }
}
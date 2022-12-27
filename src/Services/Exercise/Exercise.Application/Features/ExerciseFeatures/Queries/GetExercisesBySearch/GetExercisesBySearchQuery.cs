using Exercise.Application.Features.Views;
using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;
using Fitness.Common.Search;
using Fitness.Common.Search.SearchParameters;
using Fitness.Common.Tools;

namespace Exercise.Application.Features.ExerciseFeatures.Queries.GetExercisesBySearch;

public record GetExercisesBySearchQuery(SortModel Sort, BaseSearchQuery SearchQuery, Guid? Id, string? Name) : IQuery<List<GetExerciseBySearchViewModel>>
{
    public static GetExercisesBySearchQuery Create(GetExercisesBySearchParameters parameters)
    {
        parameters ??= new GetExercisesBySearchParameters();
        if (parameters is { Sort: null })
        {
            parameters.Sort = new SortModelParameters();
        }

        var sortModel = parameters.CheckOrAssignSortModel();
        var search = new BaseSearchQuery(parameters.PageNumber, parameters.PageSize);
        return new GetExercisesBySearchQuery(sortModel, search, parameters.Id, parameters.Name);
    }
}
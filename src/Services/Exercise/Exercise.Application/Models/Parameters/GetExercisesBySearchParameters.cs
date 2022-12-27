using Fitness.Common.Search;
using Fitness.Common.Search.SearchParameters;
using Newtonsoft.Json;

namespace Exercise.Application.Models.Parameters;

public class GetExercisesBySearchParameters : BaseSearchQueryParameters, IFilterModel
{
    public Guid? Id { get; set; }
    public string? Name { get; set; }
    public SortModelParameters Sort { get; set; }

    public GetExercisesBySearchParameters()
    {
    }

    [JsonConstructor]
    public GetExercisesBySearchParameters(Guid? id, string? name, SortModelParameters sort)
    {
        Id = id;
        Name = name;
        Sort = sort;
    }
}
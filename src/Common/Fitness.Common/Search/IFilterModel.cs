using Fitness.Common.Search.SearchParameters;

namespace Fitness.Common.Search;

public interface IFilterModel
{
    SortModelParameters Sort { get; set; }
}
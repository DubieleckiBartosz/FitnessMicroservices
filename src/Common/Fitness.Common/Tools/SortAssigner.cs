using Fitness.Common.Search; 

namespace Fitness.Common.Tools;

public static class SortAssigner
{
    public static SortModel CheckOrAssignSortModel(this IFilterModel filterModel, string? name = null, string? type = null)
    {
        var sortType = string.IsNullOrWhiteSpace(filterModel.Sort.Type) ? type ?? "desc" : filterModel.Sort.Type;
        var sortName = string.IsNullOrWhiteSpace(filterModel.Sort.Name) ? name ?? "Id" : filterModel.Sort.Name;
        sortType = sortType.ToLower() == "desc" ? "desc" : "asc";

        return new SortModel(sortName, sortType);
    }
}
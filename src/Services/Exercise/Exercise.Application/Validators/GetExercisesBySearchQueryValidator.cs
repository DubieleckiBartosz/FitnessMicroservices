using Exercise.Application.Features.ExerciseFeatures.Queries.GetExercisesBySearch;
using Fitness.Common.Validators;
using FluentValidation;

namespace Exercise.Application.Validators;

public class GetExercisesBySearchQueryValidator : AbstractValidator<GetExercisesBySearchQuery>
{
    private readonly string[] _availableName = new[]
    {
        "Name", "Created"
    };

    public GetExercisesBySearchQueryValidator()
    {
        this.When(_ => _.Sort?.Name != null,
            () => this.RuleFor(r => r.Sort).SetValidator(new SortValidator(this._availableName)!));
    }
}
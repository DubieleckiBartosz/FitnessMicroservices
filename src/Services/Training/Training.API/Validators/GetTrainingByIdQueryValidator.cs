using Fitness.Common.Core.Validators;
using FluentValidation;
using Training.API.Queries.TrainingQueries;

namespace Training.API.Validators;

public class GetTrainingByIdQueryValidator : AbstractValidator<GetTrainingByIdQuery>
{
    public GetTrainingByIdQueryValidator()
    {
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
    }
}
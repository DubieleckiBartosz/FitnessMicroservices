using Fitness.Common.Core.Validators;
using FluentValidation;

namespace Training.API.Validators;

public class AddExerciseCommandValidator : AbstractValidator<AddExerciseCommand>
{
    public AddExerciseCommandValidator()
    {
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
        RuleFor(r => r.ExternalExerciseId).SetValidator(new GuidValidator());
        RuleFor(r => r.BreakBetweenSetsInMinutes).GreaterThan(-1);
        RuleFor(r => r.NumberRepetitions).GreaterThan(0);
    }
}
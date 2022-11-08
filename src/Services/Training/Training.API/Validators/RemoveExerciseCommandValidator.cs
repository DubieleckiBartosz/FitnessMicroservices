using Fitness.Common.Core.Validators;
using FluentValidation;

namespace Training.API.Validators;

public class RemoveExerciseCommandValidator : AbstractValidator<RemoveExerciseCommand>
{
    public RemoveExerciseCommandValidator()
    {
        RuleFor(r => r.ExerciseId).SetValidator(new GuidValidator());
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
        RuleFor(r => r.NumberRepetitions).GreaterThan(0);
    }
}
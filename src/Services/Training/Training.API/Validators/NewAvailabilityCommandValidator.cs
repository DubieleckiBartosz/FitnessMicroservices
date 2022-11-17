using Fitness.Common.Core.Validators;
using FluentValidation;

namespace Training.API.Validators;

public class NewAvailabilityCommandValidator : AbstractValidator<NewAvailabilityCommand>
{
    public NewAvailabilityCommandValidator()
    {
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
        RuleFor(r => r.NewAvailability).IsInEnum();
    }
}
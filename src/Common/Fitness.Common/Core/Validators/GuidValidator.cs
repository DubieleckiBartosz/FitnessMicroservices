using FluentValidation;

namespace Fitness.Common.Core.Validators;

public class GuidValidator : AbstractValidator<Guid>
{
    public GuidValidator()
    {
        RuleFor(x => x).NotEmpty();
    }
}
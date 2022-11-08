using Fitness.Common.Core.Validators;
using FluentValidation;

namespace Training.API.Validators;

public class ShareTrainingCommandValidator : AbstractValidator<ShareTrainingCommand>
{
    public ShareTrainingCommandValidator()
    {
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
    }
}
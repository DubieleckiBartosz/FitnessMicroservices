using Fitness.Common.Validators;
using FluentValidation;
using Opinion.API.Application.Commands;

namespace Opinion.API.Application.Validators;

public class AddReactionToOpinionCommandValidator : AbstractValidatorNotNull<AddReactionToOpinionCommand>
{
    public AddReactionToOpinionCommandValidator()
    {
        RuleFor(r => r.OpinionId).GreaterThan(0);
    }
}
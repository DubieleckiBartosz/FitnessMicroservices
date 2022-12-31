using Fitness.Common.Validators;
using FluentValidation;
using Opinion.API.Application.Commands;

namespace Opinion.API.Application.Validators;

public class RemoveReactionCommandValidator : AbstractValidatorNotNull<RemoveReactionCommand>
{
    public RemoveReactionCommandValidator()
    {
        RuleFor(_ => _.ReactionId).GreaterThan(0);
    }
}
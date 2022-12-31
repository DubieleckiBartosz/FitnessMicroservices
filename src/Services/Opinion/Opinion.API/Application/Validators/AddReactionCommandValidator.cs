using Fitness.Common.Validators;
using FluentValidation;
using Opinion.API.Application.Commands;

namespace Opinion.API.Application.Validators;

public class AddReactionCommandValidator : AbstractValidatorNotNull<AddReactionCommand>
{
    public AddReactionCommandValidator()
    {
        RuleFor(r => r.ReactionFor).Must(_ => _ != default).WithMessage("{PropertyName} cannot be the default value");
    }
}
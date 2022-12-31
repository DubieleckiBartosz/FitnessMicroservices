using Fitness.Common.Validators;
using FluentValidation;
using Opinion.API.Application.Commands;

namespace Opinion.API.Application.Validators;

public class AddOpinionCommandValidator : AbstractValidatorNotNull<AddOpinionCommand>
{
    public AddOpinionCommandValidator()
    {
        RuleFor(r => r.Comment).NotEmpty().NotNull();
        RuleFor(r => r.OpinionFor).Must(_ => _ != default).WithMessage("{PropertyName} cannot be the default value");
    }
}
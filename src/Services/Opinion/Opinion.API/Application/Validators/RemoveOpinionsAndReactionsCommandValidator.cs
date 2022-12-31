using Fitness.Common.Validators;
using FluentValidation;
using Opinion.API.Application.Commands;

namespace Opinion.API.Application.Validators;

public class RemoveOpinionsAndReactionsCommandValidator : AbstractValidatorNotNull<RemoveOpinionsAndReactionsCommand>
{
    public RemoveOpinionsAndReactionsCommandValidator()
    {
        RuleFor(r => r.User).NotEmpty().NotNull();
        RuleFor(r => r.RemoveFrom).Must(_ => _ != default).WithMessage("{PropertyName} cannot be the default value");
    }
}
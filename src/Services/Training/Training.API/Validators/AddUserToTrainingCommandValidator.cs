using Fitness.Common.Core.Validators;
using FluentValidation;
using Training.API.Commands.UserCommands;

namespace Training.API.Validators;

public class AddUserToTrainingCommandValidator : AbstractValidator<AddUserToTrainingCommand>
{
    public AddUserToTrainingCommandValidator()
    {
        RuleFor(r => r.TrainingId).SetValidator(new GuidValidator());
        RuleFor(r => r.UserId).SetValidator(new GuidValidator());
    }
}
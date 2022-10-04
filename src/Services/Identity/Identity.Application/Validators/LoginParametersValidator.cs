namespace Identity.Application.Validators;

public class LoginParametersValidator : AbstractValidator<LoginParameters>
{
    public LoginParametersValidator()
    {
        RuleFor(r => r.Email).EmailValidator();
        RuleFor(r => r.Password).NotEmpty();
    }
}
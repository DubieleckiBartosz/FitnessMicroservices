namespace Identity.Application.Validators;

public class UserNewRoleParametersValidator : AbstractValidator<UserNewRoleParameters>
{
    public UserNewRoleParametersValidator()
    {
        RuleFor(r => r.Email).EmailValidator();
        RuleFor(r => r.Role).IsEnumName(typeof(Roles));
    }
}
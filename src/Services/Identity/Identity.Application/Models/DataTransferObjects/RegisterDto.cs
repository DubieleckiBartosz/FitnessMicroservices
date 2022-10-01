namespace Identity.Application.Models.DataTransferObjects;

public class RegisterDto
{
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string Password { get; }
    public string ConfirmPassword { get; }

    public RegisterDto(RegisterParameters parameters)
    {
        FirstName = parameters.FirstName;
        LastName = parameters.LastName;
        UserName = parameters.UserName;
        Email = parameters.Email;
        PhoneNumber = parameters.PhoneNumber;
        Password = parameters.Password;
        ConfirmPassword = parameters.ConfirmPassword;
    }
}
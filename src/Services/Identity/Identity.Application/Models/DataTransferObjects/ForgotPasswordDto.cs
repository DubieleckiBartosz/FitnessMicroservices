namespace Identity.Application.Models.DataTransferObjects;

public class ForgotPasswordDto
{
    public string Email { get; set; }
    public ForgotPasswordDto(ForgotPasswordParameters parameters)
    {
        this.Email = parameters.Email;
    }
}
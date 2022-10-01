namespace Identity.Application.Models.Parameters;

public class RegisterParameters
{ 
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    [JsonConstructor]
    public RegisterParameters(string firstName, string lastName, string userName,
        string email, string phoneNumber, string password, string confirmPassword)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Password = password;
        ConfirmPassword = confirmPassword;
    }
}
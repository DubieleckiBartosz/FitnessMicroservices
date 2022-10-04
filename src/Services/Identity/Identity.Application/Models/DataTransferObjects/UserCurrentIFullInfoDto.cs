namespace Identity.Application.Models.DataTransferObjects;

public class UserCurrentIFullInfoDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> Roles { get; set; }

    public UserCurrentIFullInfoDto(string firstName, string lastName, string userName, string email, string phoneNumber,
        List<string> roles)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
        Roles = roles;
    }
}
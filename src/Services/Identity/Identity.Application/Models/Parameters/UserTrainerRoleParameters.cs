namespace Identity.Application.Models.Parameters;

public class UserTrainerRoleParameters
{
    public string Email { get; set; }
    public int? YearsExperience { get; set; }

    [JsonConstructor]
    public UserTrainerRoleParameters(string email, int? yearsExperience)
    {
        Email = email;
        YearsExperience = yearsExperience;
    }
}
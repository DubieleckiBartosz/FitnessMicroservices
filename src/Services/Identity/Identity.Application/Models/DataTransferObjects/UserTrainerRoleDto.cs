namespace Identity.Application.Models.DataTransferObjects;

public class UserTrainerRoleDto
{
    public string Email { get; }
    public int? YearsExperience { get; } 
    public UserTrainerRoleDto(UserTrainerRoleParameters parameters)
    {
        Email = parameters.Email;
        YearsExperience = parameters.YearsExperience;
    }
}
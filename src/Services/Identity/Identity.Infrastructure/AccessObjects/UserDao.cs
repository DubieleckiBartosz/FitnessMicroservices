namespace Identity.Infrastructure.AccessObjects;

internal class UserDao
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool IsConfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; } 
    public string VerificationToken { get; set; }
    public DateTime? VerificationTokenExpirationDate { get; set; }
    public string ResetToken { get; set; }
    public DateTime? ResetTokenExpirationDate { get; set; }
    public int? TrainerYearsExperience { get; set; }
    public string? TrainerCode { get; set; }
    public List<int> Roles { get; set; }
    public List<TokenDao> RefreshTokens { get; set; }

    public User Map()
    {
        var roles = Roles.Select(Enumeration.GetById<Role>).ToList();
        var tokens = RefreshTokens.Select(_ => _.Map())?.ToList();
        return User.LoadUser(Id, TrainerYearsExperience, 
            TrainerCode, IsConfirmed, 
            ResetToken, ResetTokenExpirationDate,
            VerificationToken,
            VerificationTokenExpirationDate, FirstName,
            LastName, UserName, Email, PhoneNumber,
            PasswordHash, roles, tokens);
    }
}
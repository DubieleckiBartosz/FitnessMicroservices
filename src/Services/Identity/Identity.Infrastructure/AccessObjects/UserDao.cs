namespace Identity.Infrastructure.AccessObjects;

internal class UserDao
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string PasswordHash { get; set; }
    public List<int> Roles { get; set; }
    public List<TokenDao> RefreshTokens { get; set; }

    public User Map()
    {
        var roles = Roles.Select(Enumeration.GetById<Role>).ToList();
        var tokens = RefreshTokens.Select(_ => _.Map())?.ToList();
        return User.LoadUser(Id, FirstName, LastName, UserName, Email, PhoneNumber, PasswordHash, roles, tokens);
    }
}
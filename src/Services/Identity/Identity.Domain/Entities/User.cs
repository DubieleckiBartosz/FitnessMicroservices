namespace Identity.Domain.Entities;

public class User : Entity, IAggregateRoot
{  
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public string PasswordHash { get; private set; }
    public List<RefreshToken> RefreshTokens { get; private set; }
    public List<Role> Roles { get; private set; }

    private User(string firstName, string lastName, string userName, string email, string phoneNumber)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        RefreshTokens = new List<RefreshToken>();
        Roles = new List<Role> {Role.User};
    }

    private User(int id, string firstName, string lastName, string userName, string email, string phoneNumber,
        string passwordHash, List<Role> roles, List<RefreshToken>? refreshTokens = null) : this(firstName, lastName, userName, email,
        phoneNumber)
    { 
        Id = id ;

        if (refreshTokens != null && refreshTokens.Any())
        {
            RefreshTokens = refreshTokens;
        }

        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Roles = roles ?? throw new ArgumentNullException(nameof(roles));
    }

    public static User CreateUser(string firstName, string lastName, string userName, string email, string phoneNumber)
    {
        return new User(firstName, lastName, userName, email, phoneNumber);
    }

    public static User LoadUser(int id, string firstName, string lastName, string userName, string email,
        string phoneNumber,
        string passwordHash, List<Role> roles, List<RefreshToken>? refreshTokens = null)
    {
        return new User(id, firstName, lastName, userName, email,
            phoneNumber, passwordHash, roles, refreshTokens);
    }

    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void AddNewRole(Role role)
    {
        var hasThatRole = Roles.Any(_ => _ == role);
        if (hasThatRole)
        {
            throw new BusinessException(BusinessRuleErrorMessages.UniqueUserRoleErrorMessage,
                BusinessExceptionTitles.UniqueRoleTitle);
        }

        Roles.Add(role);
    }

    public RefreshToken? CurrentlyActiveRefreshToken()
    {
        var activeRefreshToken = RefreshTokens?.FirstOrDefault(_ => _.IsActive);
        return activeRefreshToken;
    }

    public RefreshToken AddNewRefreshToken(string newRefreshToken)
    {
        var activeRefreshToken = CurrentlyActiveRefreshToken();
        if (activeRefreshToken != null)
        {
            throw new BusinessException(BusinessRuleErrorMessages.OnlyOneActiveRefreshTokenErrorMessage,
                BusinessExceptionTitles.OnlyOneActiveRefreshTokenTitle);
        }

        var refreshToken = RefreshToken.CreateToken(newRefreshToken);
        RefreshTokens.Add(refreshToken);

        return refreshToken;
    }

    public RefreshToken? FindToken(string tokenKey)
    {
        var result = RefreshTokens.SingleOrDefault(_ => _.Token == tokenKey);
        return result;
    }

    public void RevokeToken(string refreshTokenKey)
    {
        var result = RefreshTokens.SingleOrDefault(_ => _.Token == refreshTokenKey);
        if (result == null)
        {
            throw new BusinessException(BusinessRuleErrorMessages.TokenNotFoundErrorMessage,
                BusinessExceptionTitles.TokenNotFoundTitle);
        }
          
        result.RevokeToken(); 
    }
}
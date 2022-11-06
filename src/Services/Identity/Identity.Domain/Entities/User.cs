namespace Identity.Domain.Entities;

public class User : Entity, IAggregateRoot
{  
    public string FirstName { get; }
    public string LastName { get; }
    public string UserName { get; }
    public string Email { get; }
    public string PhoneNumber { get; }
    public bool IsConfirmed { get; private set; } 
    public string PasswordHash { get; private set; }
    public int? TrainerYearsExperience { get; private set; }
    public string? TrainerCode { get; private set; }
    public TokenValue VerificationToken { get; private set; }
    public TokenValue? ResetToken { get; private set; }
    public List<RefreshToken> RefreshTokens { get; private set; }
    public List<Role> Roles { get; private set; }

    private User(TokenValue verificationToken, string firstName, string lastName, string userName, string email, string phoneNumber)
    {
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        UserName = userName ?? throw new ArgumentNullException(nameof(userName));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        PhoneNumber = phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber));
        VerificationToken = verificationToken; 
        IsConfirmed = false;
        ResetToken = null;
        TrainerYearsExperience = null;
        TrainerCode = null;
        RefreshTokens = new List<RefreshToken>();
        Roles = new List<Role> {Role.User};
    }

    private User(int id, int? trainerYearsExperience, string? trainerCode, bool isConfirmed, string resetToken,
        DateTime? resetTokenExpirationDate,
        string verificationToken, DateTime? verificationTokenExpirationDate, string firstName, string lastName,
        string userName,
        string email, string phoneNumber,
        string passwordHash, List<Role> roles, List<RefreshToken>? refreshTokens = null) : this(
        TokenValue.Load(verificationToken, verificationTokenExpirationDate), firstName, lastName, userName, email,
        phoneNumber)
    {
        Id = id;
        TrainerCode = trainerCode;
        TrainerYearsExperience = trainerYearsExperience;

        if (refreshTokens != null && refreshTokens.Any())
        {
            RefreshTokens = refreshTokens;
        }

        ResetToken = TokenValue.Load(resetToken, resetTokenExpirationDate);
        IsConfirmed = isConfirmed; 
        PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
        Roles = roles ?? throw new ArgumentNullException(nameof(roles));
    }

    public static User CreateUser(string verificationToken, string firstName, string lastName, string userName, string email, string phoneNumber)
    {
       var token = TokenValue.CreateVerificationToken(verificationToken);
        return new User(token, firstName, lastName, userName, email, phoneNumber);
    }

    public static User LoadUser(int id, int? trainerYearsExperience, string? trainerCode, bool isConfirmed,
        string resetToken, DateTime? resetTokenExpirationDate,
        string verificationToken, DateTime? verificationTokenExpirationDate, string firstName, string lastName,
        string userName, string email,
        string phoneNumber,
        string passwordHash, List<Role> roles, List<RefreshToken>? refreshTokens = null)
    {
        return new User(id, trainerYearsExperience, trainerCode, isConfirmed, resetToken, resetTokenExpirationDate,
            verificationToken,
            verificationTokenExpirationDate, firstName, lastName, userName, email,
            phoneNumber, passwordHash, roles, refreshTokens);
    }

    public void SetPasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
    }
    public void ChangePasswordHash(string passwordHash)
    { 
        SetPasswordHash(passwordHash);
    }
    public void ConfirmAccount()
    {
        IsConfirmed = true;
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

    public void MarkAsTrainer(int? yearsExperience)
    {
        if (TrainerCode != null)
        {
            throw new BusinessException(BusinessRuleErrorMessages.UniqueUserRoleErrorMessage,
                BusinessExceptionTitles.UniqueRoleTitle);
        }

        TrainerYearsExperience = yearsExperience;
        TrainerCode = Guid.NewGuid().ToString();

        this.AddNewRole(Role.Trainer);
    }

    public void ClearResetToken()
    {
        ResetToken = null;
    }
    public void SetResetToken(string resetTokenCode)
    {
        ResetToken = TokenValue.CreateResetToken(resetTokenCode);
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
        var result = RefreshTokens.SingleOrDefault(_ => _.TokenValue?.Token == tokenKey);
        return result;
    }

    public void RevokeToken(string refreshTokenKey)
    {
        var result = RefreshTokens.SingleOrDefault(_ => _.TokenValue?.Token == refreshTokenKey);
        if (result == null)
        {
            throw new BusinessException(BusinessRuleErrorMessages.TokenNotFoundErrorMessage,
                BusinessExceptionTitles.TokenNotFoundTitle);
        }
          
        result.RevokeToken(); 
    }
}
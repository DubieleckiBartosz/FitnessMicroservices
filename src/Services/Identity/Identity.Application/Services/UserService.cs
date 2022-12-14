using Identity.Application.Models.DataTransferObjects;
using Microsoft.AspNetCore.WebUtilities;

namespace Identity.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILoggerManager<UserService> _loggerManager;
    private readonly IIdentityEmailService _identityEmailService;
    private readonly JwtSettings _jwtSettings;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher,
        IOptions<JwtSettings> jwtSettings, ILoggerManager<UserService> loggerManager,
        IIdentityEmailService identityEmailService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
        _identityEmailService = identityEmailService ?? throw new ArgumentNullException(nameof(identityEmailService));
        _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public async Task<Response<int>> RegisterAsync(RegisterDto registerDto, string origin)
    {
        if (registerDto == null)
        {
            throw new ArgumentNullException(nameof(registerDto));
        }

        var user = await this._userRepository.FindByEmailAsync(registerDto.Email);

        if (user != null)
        {
            throw new AuthException(ExceptionIdentityMessages.UserExist(registerDto.Email),
                ExceptionIdentityTitles.UserExists);
        }

        var verificationToken = TokenUtils.RandomTokenString();
        var newUser = User.CreateUser(verificationToken, registerDto.FirstName, registerDto.LastName,
            registerDto.UserName,
            registerDto.Email, registerDto.PhoneNumber);

        var pwdHash = this._passwordHasher.HashPassword(newUser, registerDto.Password);
        newUser.SetPasswordHash(pwdHash);

        try
        {
            var identifier = await this._userRepository.CreateAsync(newUser);

            await SendVerificationEmailAsync(newUser, origin);

            return Response<int>.Ok(identifier, ResponseStrings.RegisterSuccess);
        }
        catch
        {
            this._loggerManager.LogError(new
            {
                Message = "Attempt to create a user has failed.",
                UserMail = registerDto.Email
            });

            throw;
        }
    }

    public async Task<Response<AuthenticationDto>> LoginAsync(LoginDto loginDto)
    {
        if (loginDto == null)
        {
            throw new ArgumentNullException(nameof(loginDto));
        }


        var user = await this._userRepository.FindByEmailAsync(loginDto.Email);
        if (user == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserNotFound,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.NotFound, null);
        }

        if (!user.IsConfirmed)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.AccountNotApproval,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.BadRequest, null); 
        }

        var verificationResult =
            this._passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            throw new AuthException(ExceptionIdentityMessages.IncorrectCredentials(loginDto.Email), ExceptionIdentityTitles.General);
        }

        var authenticationModel = new AuthenticationDto();
        var jwtSecurityToken = user.CreateJwtToken(_jwtSettings);
        authenticationModel.Token = jwtSecurityToken;
        authenticationModel.Email = user.Email;
        authenticationModel.UserName = user.UserName;
        authenticationModel.Roles = user.Roles.Select(_ => _.Name).ToList();

        var activeRefreshToken = user.CurrentlyActiveRefreshToken();
        if (activeRefreshToken == null)
        {
            var refreshToken = this._passwordHasher.CreateRefreshToken(user);
            var newRefreshToken = user.AddNewRefreshToken(refreshToken);
            authenticationModel.RefreshToken = newRefreshToken.GetTokenValue(); 
            authenticationModel.RefreshTokenExpiration = newRefreshToken.GetTokenExpirationDate();
            await this._userRepository.UpdateAsync(user);
        }
        else
        {
            authenticationModel.RefreshToken = activeRefreshToken.GetTokenValue();
            authenticationModel.RefreshTokenExpiration = activeRefreshToken.GetTokenExpirationDate();
        }

        return Response<AuthenticationDto>.Ok(authenticationModel);
    }

    public async Task<Response<string>> AddToRoleAsync(UserNewRoleDto userNewRoleDto)
    {
        if (userNewRoleDto == null)
        {
            throw new ArgumentNullException(nameof(userNewRoleDto));
        }

        var user = await this._userRepository.FindByEmailAsync(userNewRoleDto.Email);
        if (user == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserNotFound,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.NotFound, null);
        }

        if (!user.IsConfirmed)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.AccountNotApproval,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.BadRequest, null);
        }

        var allRoles = EnumUtils.GetStringValuesFromEnum<Roles>();
        var newRole = allRoles.FirstOrDefault(_ =>
            _.ToLower() == userNewRoleDto.Role.ToLower());

        if (newRole == null)
        {
            throw new BadRequestException(ExceptionIdentityMessages.RoleNotFound(allRoles),
                ExceptionIdentityTitles.RoleDoesNotExist);
        }
         
        user.AddNewRole(Enumeration.GetById<Role>((int)newRole.ToEnum<Roles>())); 

        await this._userRepository.AddToRoleAsync(user);

        return Response<string>.Ok(ResponseStrings.OperationSuccess);
    }

    public async Task<Response<string>> AddToTrainerRoleAsync(UserTrainerRoleDto userTrainerRoleDto)
    {
        if (userTrainerRoleDto == null)
        {
            throw new ArgumentNullException(nameof(userTrainerRoleDto));
        }

        var user = await this._userRepository.FindByEmailAsync(userTrainerRoleDto.Email);
        if (user == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserNotFound,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.NotFound, null);
        }

        if (!user.IsConfirmed)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.AccountNotApproval,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.BadRequest, null);
        }
        
        user.MarkAsTrainer(userTrainerRoleDto.YearsExperience);

        await this._userRepository.AddToRoleAsync(user);

        return Response<string>.Ok(ResponseStrings.OperationSuccess);
    }

    public async Task<Response<AuthenticationDto>> RefreshTokenAsync(string refreshTokenKey)
    {
        if (string.IsNullOrEmpty(refreshTokenKey))
        {
            throw new BadRequestException(ExceptionIdentityMessages.TokenIsEmptyOrNull,
                ExceptionIdentityTitles.ValidationError);
        }

        var user = await _userRepository.FindUserByTokenAsync(refreshTokenKey);

        var refreshToken = user.FindToken(refreshTokenKey);
        if (refreshToken == null || !refreshToken.IsActive)
        {
            throw new AuthException(ExceptionIdentityMessages.TokenNotActive, ExceptionIdentityTitles.ValidationError);
        }
        var newRefreshToken = this._passwordHasher.CreateRefreshToken(user);

        user.RevokeToken(refreshToken.GetTokenValue());
        refreshToken.ReplaceToken(newRefreshToken);
        var newUserRefreshToken = user.AddNewRefreshToken(newRefreshToken);

        await _userRepository.UpdateAsync(user);

        var jwtSecurityToken = user.CreateJwtToken(_jwtSettings);
        var roles = user.Roles.Select(_ => _.Name).ToList();
        var responseModel = new AuthenticationDto(user.UserName, user.Email, roles, jwtSecurityToken, newRefreshToken,
            newUserRefreshToken.GetTokenExpirationDate());

        return Response<AuthenticationDto>.Ok(responseModel);
    }
 

    public async Task<Response<string>> RevokeTokenAsync(string tokenKey)
    {
        if (string.IsNullOrEmpty(tokenKey))
        {
            throw new AuthException(ExceptionIdentityMessages.TokenIsEmptyOrNull, ExceptionIdentityTitles.ValidationError);
        }

        var user = await this._userRepository.FindUserByTokenAsync(tokenKey);

        var refreshToken = user.FindToken(tokenKey);
        if (refreshToken == null || !refreshToken.IsActive)
        {
            throw new AuthException(ExceptionIdentityMessages.TokenNotActive, null);
        }

        refreshToken.RevokeToken();
        await this._userRepository.UpdateAsync(user);

        return Response<string>.Ok(ResponseStrings.OperationSuccess);
    }

    public async Task<Response<string>> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, string origin)
    {
        var user = await _userRepository.FindByEmailAsync(forgotPasswordDto.Email);

        if (user == null)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.UserNotFound,
                ExceptionIdentityTitles.UserByEmail, HttpStatusCode.NotFound, null);
        }

        var newToken = TokenUtils.RandomTokenString();
        user.SetResetToken(newToken);

        await _userRepository.UpdateAsync(user);

        await _identityEmailService.SendEmailResetPasswordAsync(user.Email, newToken, origin);

        return Response<string>.Ok(ResponseStrings.SentLinkResetTokenSuccess);
    }

    public async Task<Response<UserCurrentIFullInfoDto>> GetCurrentUserInfoAsync(string token)
    {
        var user = await this._userRepository.FindUserByTokenAsync(token); 

        var roles = user.Roles.Select(_ => _.Name).ToList();
        var modelResponse =
            new UserCurrentIFullInfoDto(user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, roles);

        return Response<UserCurrentIFullInfoDto>.Ok(modelResponse);
    }

    public async Task<Response<string>> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var user = await _userRepository.FindUserByResetTokenAsync(resetPasswordDto.Token);
        if (user?.ResetToken?.IsActive == false)
        {
            throw new IdentityResultException(ExceptionIdentityMessages.ResetTokenExpired,
                ExceptionIdentityTitles.ResetToken, HttpStatusCode.InternalServerError, null);
        }

        var pwdHash = this._passwordHasher.HashPassword(user, resetPasswordDto.Password);
        
        user.ChangePasswordHash(pwdHash);
        user.ClearResetToken();

        await _userRepository.UpdateAsync(user);

        return Response<string>.Ok(ResponseStrings.PasswordChangedSuccess);
    }

    public async Task<Response<string>> VerifyEmail(VerifyAccountDto verifyAccountDto)
    {
        var tokenCode = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(verifyAccountDto.Token));
        var user = await _userRepository.FindUserByVerificationTokenAsync(tokenCode);

        user.ConfirmAccount();

        _loggerManager.LogInformation("Account has been confirmed");
        
        await _userRepository.ConfirmAccountAsync(user);

        _loggerManager.LogInformation("Changes have been saved");

        return Response<string>.Ok(ResponseStrings.VerificationAccountSuccess);
    }

    private async Task SendVerificationEmailAsync(User user, string origin)
    {
        var tokenCode = user.VerificationToken.Token;
        tokenCode = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenCode));

        var routeUri = new Uri(string.Concat($"{origin}/", "api/account/confirm-account/"));
        var verificationUri = QueryHelpers.AddQueryString(routeUri.ToString(), "code", tokenCode);

        await _identityEmailService.SendEmailAfterCreateNewAccountAsync(user.Email, verificationUri, user.UserName);
    }
}
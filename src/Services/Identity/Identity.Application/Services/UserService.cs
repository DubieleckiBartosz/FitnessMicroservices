namespace Identity.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ILoggerManager<UserService> _loggerManager;
    private readonly JwtSettings _jwtSettings;

    public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IOptions<JwtSettings> jwtSettings, ILoggerManager<UserService> loggerManager)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _loggerManager = loggerManager ?? throw new ArgumentNullException(nameof(loggerManager));
        _jwtSettings = jwtSettings?.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
    }

    public async Task<Response<int>> RegisterAsync(RegisterDto registerDto)
    {
        if (registerDto == null)
        {
            throw new ArgumentNullException(nameof(registerDto));
        }

        var user = await this._userRepository.FindByEmailAsync(registerDto.Email, false);

        if (user != null)
        {
            throw new AuthException(ExceptionIdentityMessages.UserExist(registerDto.Email),
                ExceptionIdentityTitles.UserExists);
        }

        var newUser = User.CreateUser(registerDto.FirstName, registerDto.LastName, registerDto.UserName,
            registerDto.Email, registerDto.PhoneNumber);

        var pwdHash = this._passwordHasher.HashPassword(newUser, registerDto.Password);
        newUser.SetPasswordHash(pwdHash);

        try
        {
            var identifier = await this._userRepository.CreateAsync(newUser); 

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
            authenticationModel.RefreshToken = newRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = newRefreshToken.Expires; 
            await this._userRepository.UpdateAsync(user);
        }
        else
        {
            authenticationModel.RefreshToken = activeRefreshToken.Token;
            authenticationModel.RefreshTokenExpiration = activeRefreshToken.Expires;
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

    public async Task<Response<AuthenticationDto>> RefreshTokenAsync(string refreshTokenKey)
    {
        var user = await _userRepository.FindUserByTokenAsync(refreshTokenKey);

        var refreshToken = user.FindToken(refreshTokenKey);
        if (refreshToken == null || !refreshToken.IsActive)
        {
            throw new AuthException(ExceptionIdentityMessages.TokenNotActive, ExceptionIdentityTitles.ValidationError);
        }
        var newRefreshToken = this._passwordHasher.CreateRefreshToken(user);

        user.RevokeToken(refreshToken.Token);
        refreshToken.ReplaceToken(newRefreshToken);
        var newUserRefreshToken = user.AddNewRefreshToken(newRefreshToken);

        await _userRepository.UpdateAsync(user);

        var jwtSecurityToken = user.CreateJwtToken(_jwtSettings);
        var roles = user.Roles.Select(_ => _.Name).ToList();
        var responseModel = new AuthenticationDto(user.UserName, user.Email, roles, jwtSecurityToken, newRefreshToken,
            newUserRefreshToken.Expires);

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

    public async Task<Response<UserCurrentIFullInfoDto>> GetCurrentUserInfoAsync(string token)
    {
        var user = await this._userRepository.FindUserByTokenAsync(token); 

        var roles = user.Roles.Select(_ => _.Name).ToList();
        var modelResponse =
            new UserCurrentIFullInfoDto(user.FirstName, user.LastName, user.UserName, user.Email, user.PhoneNumber, roles);

        return Response<UserCurrentIFullInfoDto>.Ok(modelResponse);
    }
}
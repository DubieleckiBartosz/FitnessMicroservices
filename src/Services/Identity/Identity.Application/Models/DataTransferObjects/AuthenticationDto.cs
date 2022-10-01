namespace Identity.Application.Models.DataTransferObjects;

public class AuthenticationDto
{ 
    public string UserName { get; }
    public string Email { get;  }
    public List<string> Roles { get; }
    public string Token { get;} 
    public string RefreshToken { get; }
    public DateTime RefreshTokenExpiration { get;}
    public AuthenticationDto(AuthenticationParameters parameters)
    {
        UserName = parameters.UserName;
        Email = parameters.Email;
        Roles = parameters.Roles;
        Token = parameters.Token;
        RefreshToken = parameters.RefreshToken;
        RefreshTokenExpiration = parameters.RefreshTokenExpiration;
    }
}
namespace Identity.Application.Models.Parameters;

public class AuthenticationParameters
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }
    [JsonIgnore]
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
    [JsonConstructor]
    public AuthenticationParameters(string userName, string email, List<string> roles, string token, string refreshToken, DateTime refreshTokenExpiration)
    {
        UserName = userName;
        Email = email;
        Roles = roles;
        Token = token;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
    }
}
namespace Identity.Infrastructure.AccessObjects;

internal class TokenDao
{
    public int Id { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    public DateTime Created { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? Revoked { get; set; }

    public RefreshToken Map()
    {
        return RefreshToken.LoadToken(Id, Token, Expires, Created, ReplacedByToken, TokenActivity.Load(Revoked));
    }
}
namespace Identity.Domain.Entities;

public class RefreshToken : Entity
{
    public string Token { get; }
    public DateTime Expires { get; }
    public DateTime Created { get; }
    public string? ReplacedByToken { get; private set; }
    public TokenActivity TokenActivity { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => TokenActivity.Revoked == null && !IsExpired;

    private RefreshToken(string token)
    {
        Token = token;
        Expires = DateTime.UtcNow.AddDays(5);
        Created = DateTime.UtcNow;
        TokenActivity = TokenActivity.IsNotRevoked();
    }

    private RefreshToken(int id, string token, DateTime expires, DateTime created, string? replacedByToken,
        TokenActivity tokenActivity) :
        this(token)
    {
        Id = id;
        Expires = expires;
        Created = created;
        ReplacedByToken = replacedByToken;
        TokenActivity = tokenActivity;
    }

    public static RefreshToken LoadToken(int id, string token, DateTime expires, DateTime created, string? replacedByToken,
        TokenActivity tokenActivity)
    {
        return new RefreshToken(id, token, expires, created, replacedByToken, tokenActivity);
    }

    public static RefreshToken CreateToken(string token)
    {
        return new RefreshToken(token);
    }

    public void ReplaceToken(string newToken)
    {
        ReplacedByToken = newToken;
    }

    public void RevokeToken()
    {
        TokenActivity = TokenActivity.IsRevoked();
    }
}
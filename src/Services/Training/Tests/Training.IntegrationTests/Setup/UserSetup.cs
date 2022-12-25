using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Training.IntegrationTests.Data.Fake;

namespace Training.IntegrationTests.Setup;

public class UserSetup
{
    public static ClaimsPrincipal UserPrincipals(bool isTrainer = true)
    {
        var claimsPrincipal = new ClaimsPrincipal();
        var firstName = $"SuperUser_FirstName_Test";
        var lastName = $"SuperUser_LastName_Test";
        var userName = $"SuperUserTest";

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Role, FakeRoles.User.ToString()), 
            new Claim(ClaimTypes.Role, FakeRoles.Trainer.ToString()),
            new Claim(ClaimTypes.Role, FakeRoles.Admin.ToString()),
            new Claim(ClaimTypes.Name, $"{firstName}_{lastName}_{userName}"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Email, "SuperUser@test.com"),
            new Claim(ClaimTypes.NameIdentifier, "1")
        };

        if (isTrainer)
        {
            claims.Add(new Claim("trainer_code", Guid.NewGuid().ToString()));
        }

        claimsPrincipal.AddIdentity(new ClaimsIdentity(claims));

        return claimsPrincipal;
    }
}
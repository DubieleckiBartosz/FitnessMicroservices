namespace Identity.Application.IdentityTemplates;

public class Templates
{
    public static string GetConfirmAccountTemplate()
    {
        return "<!DOCTYPE html><html><body><h2>Hi {UserName}</h2></br><p><strong>" +
               "Confirm your registration:<strong> " +
               "<a href={VerificationUri}>" +
               "confirmation</a></p></body></html>";
    }
    
    public static string GetResetPasswordTemplate()
    {
        return "<!DOCTYPE html><html><body><h4>Reset Password Email</h4>" +
               "<p>Please use the below token to reset your password with the <code>" +
               "{origin}/user/reset-password</code> api route: </p>" +
               "<p><code>{resetToken}</code></p></body></html>";
    }
}
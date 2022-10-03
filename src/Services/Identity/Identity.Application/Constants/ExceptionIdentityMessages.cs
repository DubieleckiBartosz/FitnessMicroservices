namespace Identity.Application.Constants;

public class ExceptionIdentityMessages
{
    public static string UserExist(string email)
    {
        return $"Email {email} is already registered.";
    }

    public static string IncorrectCredentials(string email)
    {
        return $"Incorrect credentials for user {email}.";
    }

    public const string RegisterFailed = "Cannot create user, check the data and try again later."; 
    public const string UserNotFound = "User not found.";
    public const string UserRolesNotFound = "Unable to get user roles.";
    public const string IncorrectData = "Incorrect data! Check data and try again.";

    public static string RoleNotFound(List<string> allRoles)
    {
        return $"Role not found. You can choose one of the roles: {string.Join($", {Environment.NewLine}", allRoles)}";
    }
    
    public const string RefreshTokenNotFound = "Refresh token was not found."; 
    public const string UserUpdateDataFailed = "User data could not be updated.";
    public const string NewRoleForUserFailed = "Failed to create a new role for a user.";
    public const string TokenNotMatch = "Token did not match any users.";
    public const string TokenNotActive = "Token is not active.";
    public const string TokenIsEmptyOrNull = "Token is null or empty.";
    public const string NoPermission = "You are not authorized to perform this operation!.";
} 
namespace Enrollment.Application.Constants;

public class Strings
{
    //Messages
    public const string UserHasEnrollmentMessage = "User cannot be enrolled twice in the same training.";
    public const string EnrollmentsMustBeOpenMessage = "Cannot save when enrollments have status other than open.";
    public const string UserEnrollmentNotFoundMessage = "User enrollment must exists for this operation.";
    public const string NoPermissionsMessage = "Only the creator can do this.";
    public const string UserEnrollmentIsCancelledMessage = "User enrollment cannot be accepted when it is cancelled.";
    public const string InvalidStatusMessage = "Enrollment must be open or suspended.";

    //Titles
    public const string UserHasEnrollmentTitle = "User is already enrolled in this training.";
    public const string EnrollmentsMustBeOpenTitle = "Enrollments must be open.";
    public const string UserEnrollmentNotFoundTitle = "User enrollment not found.";
    public const string NoPermissionsTitle = "No permissions.";
    public const string UserEnrollmentIsCancelledTitle = "User enrollment is cancelled.";
    public const string InvalidStatusTitle = "Invalid status.";
}
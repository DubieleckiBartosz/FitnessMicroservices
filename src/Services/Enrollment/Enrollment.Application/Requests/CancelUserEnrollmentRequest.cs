using Newtonsoft.Json;

namespace Enrollment.Application.Requests;

public class CancelUserEnrollmentRequest
{ 
    public Guid EnrollmentId { get; init; }
    public Guid UserEnrollment { get; init; }

    [JsonConstructor]
    public CancelUserEnrollmentRequest(Guid enrollmentId, Guid userEnrollment)
    {
        EnrollmentId = enrollmentId;
        UserEnrollment = userEnrollment;
    }
}
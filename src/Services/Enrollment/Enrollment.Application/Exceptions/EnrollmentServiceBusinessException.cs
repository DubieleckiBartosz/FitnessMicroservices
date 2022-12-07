using Microsoft.AspNetCore.Http;

namespace Enrollment.Application.Exceptions;

public class EnrollmentServiceBusinessException : Exception
{
    public int StatusCode { get; }
    public string Title { get; }

    public EnrollmentServiceBusinessException(string title, string message,
        int statusCode = StatusCodes.Status400BadRequest) :
        base(message)
    {
        StatusCode = statusCode;
        Title = title;
    }
}
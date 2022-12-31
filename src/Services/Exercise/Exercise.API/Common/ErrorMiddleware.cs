using Exercise.Domain.Base;
using Fitness.Common.Core;

namespace Exercise.API.Common;

public class ErrorMiddleware
{
    public static object GetErrorResponse(Exception exception, int statusCode)
    {
        var title = GetTitle(exception);
        var response = new
        {
            title = string.IsNullOrEmpty(title) ? ErrorHandlingMiddleware.GetGlobalTitle(exception) : title,
            status = statusCode,
            detail = exception.Message,
            errors = ErrorHandlingMiddleware.AssignErrors(exception)
        };

        return response;
    }

    public static int GetStatusCode(Exception exception)
    {
        var statusCode = exception switch
        {
            ExerciseBusinessException e => (int)e.StatusCode,
            _ => 0
        };

        return statusCode;
    }


    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ExerciseBusinessException e => e.Title,
            _ => string.Empty
        };
}
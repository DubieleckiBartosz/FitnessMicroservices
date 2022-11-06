using Training.API.Common.Exceptions;

namespace Training.API.Common;

public static class ErrorMiddleware
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
            TrainingServiceBusinessException e => e.StatusCode,
            _ => 0
        };

        return statusCode;
    }


    private static string GetTitle(Exception exception) =>
        exception switch
        {
            TrainingServiceBusinessException e => e.Title,
            _ => string.Empty
        };
}
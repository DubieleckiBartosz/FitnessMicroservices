namespace Common.Infrastructure.Core.Middlewares;
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly Func<Exception, int> _funcStatusCode;
    private readonly Func<Exception, int, object> _funcResponse;

    public ErrorHandlingMiddleware(RequestDelegate next, Func<Exception, int> funcStatusCode,
        Func<Exception, int, object> funcResponse)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _funcStatusCode = funcStatusCode ?? throw new ArgumentNullException(nameof(funcStatusCode));
        _funcResponse = funcResponse ?? throw new ArgumentNullException(nameof(funcResponse));
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this._next(context);
        }
        catch (Exception e)
        {
            //Log

            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = _funcStatusCode.Invoke(exception);
        if (statusCode == 0)
        {
            statusCode = GetStatusCode(exception);
        }


        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        var response = _funcResponse.Invoke(exception, statusCode);

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            ArgumentNullException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder,
        Func<Exception, int> funcStatusCode, Func<Exception, int, object> funcResponse)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>(funcStatusCode, funcResponse);
    }
}
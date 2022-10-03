namespace Fitness.Common.Logging
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerManager<LoggingMiddleware> loggerManager)
        {
            this.LogRequest(context, loggerManager);
            await _next(context);
            this.LogResponse(context, loggerManager);
        }

        private void LogRequest(HttpContext context, ILoggerManager<LoggingMiddleware> loggerManager)
        { 
            loggerManager.LogInformation(new
            {
                LogType = "Http Request Information",
                Schema = context.Request.Scheme,
                Host = context.Request.Host,
                Path = context.Request.Path,
                Query = context.Request.QueryString
            });
        }
        private void LogResponse(HttpContext context, ILoggerManager<LoggingMiddleware> loggerManager)
        {
            loggerManager.LogInformation(new
            {
                LogType = "Http Response Information", 
                Path = context.Request?.Path, 
                StatusCode = context.Response?.StatusCode
            }); 

            //Body 
        }
    }

    public static class LoggingHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggerHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}

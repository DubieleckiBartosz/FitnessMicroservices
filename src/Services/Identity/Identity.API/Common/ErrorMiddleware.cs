using Identity.Application.Common.Exceptions;
using Identity.Application.Constants; 

namespace Identity.API.Common
{
    public class ErrorMiddleware
    {
        public static object GetErrorResponse(Exception exception, int statusCode)
        {
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
                errors = GetErrors(exception)
            };

            return response;
        }

        public static int GetStatusCode(Exception exception)
        {
            var statusCode = exception switch
            {
                IdentityResultException identityResultException => (int)identityResultException.StatusCode,
                IdentityApplicationException applicationException => (int)applicationException.StatusCode,
                _ => 0
            };

            return statusCode;
        }

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                IdentityResultException identityResultException => identityResultException.Title,
                IdentityApplicationException applicationException => applicationException.Title,
                _ => ResponseMessages.ServerError
            };

        private static IReadOnlyList<string>? GetErrors(Exception exception)
        {
            IReadOnlyList<string>? errors = null;

            if (exception is IdentityResultException validationException)
            {
                errors = validationException.Errors;
            }

            return errors;
        }
    }
}
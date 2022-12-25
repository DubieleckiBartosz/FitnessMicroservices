using Microsoft.AspNetCore.Mvc.Filters;
using Training.IntegrationTests.Setup;

namespace Training.IntegrationTests.Common;

public class FakeUserFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        context.HttpContext.User = UserSetup.UserPrincipals();

        await next();
    }
}
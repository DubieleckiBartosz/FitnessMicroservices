using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected readonly ICommandBus CommandBus;
    protected readonly IQueryBus QueryBus;

    public BaseController(ICommandBus commandBus, IQueryBus queryBus)
    {
        CommandBus = commandBus;
        QueryBus = queryBus;
    }
}
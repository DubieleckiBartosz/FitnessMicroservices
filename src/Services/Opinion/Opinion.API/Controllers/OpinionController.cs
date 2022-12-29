using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Mvc; 

namespace Opinion.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OpinionController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public OpinionController(ICommandBus commandBus, IQueryBus queryBus)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
    }
}
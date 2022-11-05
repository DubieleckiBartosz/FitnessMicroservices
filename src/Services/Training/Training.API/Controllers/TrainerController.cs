using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Training.API.Commands.TrainerCommands;
using Training.API.Requests.TrainerRequests;

namespace Training.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainerController : ControllerBase
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public TrainerController(IQueryBus queryBus, ICommandBus commandBus)
    {
        _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewTrainer(AddNewTrainerInfoRequest request)
    {
        var command = AddNewTrainerInfoCommand.Create(request);
        var result = await _commandBus.Send(command);

        return Ok(result);
    }
}
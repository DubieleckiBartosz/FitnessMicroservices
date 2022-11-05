using System.Security.Claims;
using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Training.API.Commands.ExerciseCommands;
using Training.API.Commands.TrainingCommands;
using Training.API.Requests;

namespace Training.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TrainingController : ControllerBase
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public TrainingController(IQueryBus queryBus, ICommandBus commandBus)
    {
        _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> InitTraining()
    {
        var currentUserId = HttpContext.User.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(currentUserId, out var userId))
        {

            var command = TrainingInitiationCommand.Create(userId);
            var resultTrainingId = await _commandBus.Send(command);

            return Ok(resultTrainingId);
        }

        return BadRequest();
    }

    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewExercise([FromBody] AddExerciseRequest request)
    {
        var command = AddExerciseCommand.Create(request);
        await _commandBus.Send(command);

        return Ok();
    }
}
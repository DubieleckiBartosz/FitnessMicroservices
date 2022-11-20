using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Training.API.Handlers.ViewModels;
using Training.API.Queries.TrainingQueries;

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

    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(TrainingDetailsViewModel), 200)]
    [SwaggerOperation(Summary = "Get training by identifier")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpGet("[action]/{trainingId}")]
    public async Task<IActionResult> GetTraining([FromRoute] Guid trainingId)
    {
        var query = GetTrainingByIdQuery.Create(trainingId);
        var resultQuery = await _queryBus.Send(query);

        return Ok(resultQuery);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Guid), 200)]
    [SwaggerOperation(Summary = "Init training")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpPost("[action]")]
    public async Task<IActionResult> InitTraining()
    {
        var command = TrainingInitiationCommand.Create();
        var resultTrainingId = await _commandBus.Send(command);

        return Ok(resultTrainingId);
    }
    
    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(Guid), 200)]
    [SwaggerOperation(Summary = "Share training")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpPost("[action]")]
    public async Task<IActionResult> ShareTraining([FromBody] ShareTrainingRequest request)
    {
        var command = ShareTrainingCommand.Create(request);
        await _commandBus.Send(command);

        return Ok();
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Add new exercise to training")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddNewExercise([FromBody] AddExerciseRequest request)
    { 
        var command = AddExerciseCommand.Create(request);
        await _commandBus.Send(command);

        return Ok();
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Remove exercise")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpPost("[action]")]
    public async Task<IActionResult> RemoveExercise([FromBody] RemoveExerciseRequest request)
    {
        var command = RemoveExerciseCommand.Create(request);
        await _commandBus.Send(command);

        return Ok();
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Change training availability")]
    [Authorize(Roles = Strings.TrainerRole)]
    [HttpPut("[action]")]
    public async Task<IActionResult> NewAvailability([FromBody] NewAvailabilityRequest request)
    {
        var command = NewAvailabilityCommand.Create(request);
        await _commandBus.Send(command);

        return Ok();
    }
}
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
}
using Exercise.Application.Features.ExerciseFeatures.Commands.AddNewImageToExercise;
using Exercise.Application.Features.ExerciseFeatures.Commands.CreateNewExercise;
using Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseDescription;
using Exercise.Application.Features.ExerciseFeatures.Commands.UpdateExerciseVideoLink;
using Exercise.Application.Features.ExerciseFeatures.Queries.GetExerciseById;
using Exercise.Application.Features.ExerciseFeatures.Queries.GetExercisesBySearch;
using Exercise.Application.Features.Views;
using Exercise.Application.Models.Parameters;
using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Exercise.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExerciseController : ControllerBase
{
    private readonly ICommandBus _commandBus;
    private readonly IQueryBus _queryBus;

    public ExerciseController(ICommandBus commandBus, IQueryBus queryBus)
    {
        _commandBus = commandBus ?? throw new ArgumentNullException(nameof(commandBus));
        _queryBus = queryBus ?? throw new ArgumentNullException(nameof(queryBus));
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200, Type = typeof(GetExerciseByIdViewModel))]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Get exercise details by id")]
    [HttpGet("[action]/{exerciseId}")]
    public async Task<IActionResult> GetDetailsById([FromRoute] Guid exerciseId)
    {
        var query = GetExerciseByIdQuery.Create(exerciseId);
        var result = await _queryBus.Send(query);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200, Type = typeof(List<GetExerciseBySearchViewModel>))]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Get exercises by search")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBySearch([FromBody] GetExercisesBySearchParameters parameters)
    {
        var query = GetExercisesBySearchQuery.Create(parameters);

        var result = await _queryBus.Send(query);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200, Type = typeof(Guid))]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Create new exercise")]
    [Authorize(Roles = "Trainer")]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewExercise([FromBody] CreateNewExerciseParameters parameters)
    {
        var command = CreateNewExerciseCommand.Create(parameters);

        var result = await _commandBus.Send(command);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Update exercise")]
    [Authorize(Roles = "Trainer")]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateExerciseDescription(
        [FromBody] UpdateExerciseDescriptionParameters parameters)
    {
        var command = UpdateExerciseDescriptionCommand.Create(parameters);

        await _commandBus.Send(command);

        return NoContent();
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(200, Type = typeof(Guid))]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Add new image to exercise")]
    [Authorize(Roles = "Trainer")]
    [HttpPut("[action]")]
    public async Task<IActionResult> CreateImageForExercise([FromForm] AddNewImageToExerciseParameters parameters)
    {
        var command = AddNewImageToExerciseCommand.Create(parameters);

        var result = await _commandBus.Send(command);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "New video link for exercise")]
    [Authorize(Roles = "Trainer")]
    [HttpPut("[action]")]
    public async Task<IActionResult> NewVideoLinkExercise([FromBody] UpdateExerciseVideoLinkParameters parameters)
    {
        var command = UpdateExerciseVideoLinkCommand.Create(parameters);

        await _commandBus.Send(command);

        return NoContent();
    }
}
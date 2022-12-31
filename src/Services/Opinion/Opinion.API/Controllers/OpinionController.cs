using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Opinion.API.Application.Commands;
using Opinion.API.Application.Parameters;
using Opinion.API.Application.Queries;
using Opinion.API.Application.Views;
using Opinion.API.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

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
    
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(ResponseData<GetOpinionsAndReactionsForExternalDataViewModel>), 200)]
    [SwaggerOperation(Summary = "Get opinions and reactions")]
    [HttpGet("[action]/{dataId}")]
    public async Task<IActionResult> GetOpinionWithReactions([FromRoute] Guid dataId)
    {
        var query = GetOpinionsForExternalEntityQuery.Create(dataId);
        var response = await _queryBus.Send(query);

        return Ok(response);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(ResponseData<long>), 200)]
    [SwaggerOperation(Summary = "Add new opinion")]
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddOpinion([FromBody] AddOpinionParameters parameters)
    {
        var command = AddOpinionCommand.Create(parameters);
        var result = await _commandBus.Send(command);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(ResponseData<long>), 200)]
    [SwaggerOperation(Summary = "Add new reaction")]
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddReaction([FromBody] AddReactionParameters parameters)
    {
        var command = AddReactionCommand.Create(parameters);
        var result  = await _commandBus.Send(command);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType(typeof(ResponseData<long>), 200)]
    [SwaggerOperation(Summary = "Add reaction to opinion")]
    [Authorize]
    [HttpPost("[action]")]
    public async Task<IActionResult> AddReactionToOpinion([FromBody] AddReactionToOpinionParameters parameters)
    {
        var command = AddReactionToOpinionCommand.Create(parameters);
        var result  = await _commandBus.Send(command);

        return Ok(result);
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [ProducesResponseType( 204)]
    [SwaggerOperation(Summary = "Remove reaction")]
    [Authorize]
    [HttpDelete("[action]")]
    public async Task<IActionResult> RemoveReaction([FromBody] RemoveReactionParameters parameters)
    {
        var command = RemoveReactionCommand.Create(parameters);
        await _commandBus.Send(command);

        return NoContent();
    } 
}
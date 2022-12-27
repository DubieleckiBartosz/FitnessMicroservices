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
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Get exercise details by id")]
    [HttpGet("[action]/{exerciseId}")]
    public async Task<IActionResult> GetDetailsById([FromRoute] Guid exerciseId)
    {
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Get exercises by search")]
    [HttpGet("[action]")]
    public async Task<IActionResult> GetBySearch([FromBody])
    {
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Create new exercise")]
    [Authorize(Roles = )]
    [HttpPost("[action]")]
    public async Task<IActionResult> CreateNewExercise([FromBody]  )
    {  
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Update exercise")]
    [Authorize(Roles = )]
    [HttpPut("[action]")]
    public async Task<IActionResult> UpdateExerciseDescription([FromBody]  )
    {
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "Add new image to exercise")]
    [Authorize(Roles = )]
    [HttpPut("[action]")]
    public async Task<IActionResult> CreateImageForExercise([[FromForm]]  )
    {
    }

    [ProducesResponseType(401)]
    [ProducesResponseType(204)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 500)]
    [SwaggerOperation(Summary = "New video link for exercise")]
    [Authorize(Roles = )]
    [HttpPut("[action]")]
    public async Task<IActionResult> NewVideoLinkExercise([FromBody]  )
    {
    }
}
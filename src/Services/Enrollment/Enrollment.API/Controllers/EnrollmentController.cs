using Enrollment.Application.Commands;
using Enrollment.Application.Requests;
using Fitness.Common.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : BaseController
    {
        public EnrollmentController(ICommandBus commandBus, IQueryBus queryBus) : base(commandBus, queryBus)
        {
        }

        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(object), 500)]
        [ProducesResponseType( 204)] 
        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> Test([FromBody] CancelUserEnrollmentRequest request)
        {
            var command = CancelUserEnrollmentCommand.Create(request.EnrollmentId, request.UserEnrollment);
            await CommandBus.Send(command);
            return NoContent();
        }

      
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Enrollment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        //New enrollment
        //Remove enrollment
        //Get enrollments
        public EnrollmentController()
        {
            
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(new {message = "ok."});
        }
    }
}

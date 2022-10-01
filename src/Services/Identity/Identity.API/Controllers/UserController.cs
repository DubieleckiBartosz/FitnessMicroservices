using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public UserController()
        {
            
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUser()
        {
            return Ok("OK");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser()
        {
            return Ok("OK");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken()
        {
            return Ok("OK");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ForgotPassword()
        {
            return Ok("OK");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmAccount()
        {
            return Ok("OK");
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> KillToken()
        {
            return Ok("OK");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> AddNewRoleToUser()
        {
            return Ok("OK");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUserData()
        {
            return Ok("OK");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUserDetails()
        {
            return Ok("OK");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok("OK");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetUsersWithDetails()
        {
            return Ok("OK");
        }
    }
}

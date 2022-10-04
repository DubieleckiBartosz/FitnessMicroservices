namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<int>), 200)]
        [SwaggerOperation(Summary = "Register user")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterParameters parameters)
        {
            var response = await _userService.RegisterAsync(new RegisterDto(parameters));
            return Ok(response);
        }

        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<AuthenticationDto>), 200)]
        [SwaggerOperation(Summary = "User login")]
        [HttpPost("[action]")]
        public async Task<IActionResult> LoginUser([FromBody] LoginParameters parameters)
        {
            var response = await _userService.LoginAsync(new LoginDto(parameters));
            this.SetRefreshTokenInCookie(response.Data.RefreshToken);

            return Ok(response);
        }

        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<AuthenticationDto>), 200)]
        [SwaggerOperation(Summary = "Refresh token")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies[ConstantKeys.CookieRefreshToken];
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var response = await _userService.RefreshTokenAsync(refreshToken);
            if (!string.IsNullOrEmpty(response.Data.RefreshToken))
            {
                this.SetRefreshTokenInCookie(response.Data.RefreshToken);
            }

            return Ok(response);
        }

        //[HttpPost("[action]")]
        //public async Task<IActionResult> ForgotPassword()
        //{
        //    return Ok("OK");
        //}

        //[HttpPost("[action]")]
        //public async Task<IActionResult> ConfirmAccount()
        //{
        //    return Ok("OK");
        //}

        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [SwaggerOperation(Summary = "Revoke token")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeToken()
        {
            var refreshToken = Request.Cookies[ConstantKeys.CookieRefreshToken];
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var result = await this._userService.RevokeTokenAsync(refreshToken);
            return Ok(result);
        }

        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<string>), 200)]
        [SwaggerOperation(Summary = "Add user to new role")]
        [HttpPut("[action]")]
        public async Task<IActionResult> AddNewRoleToUser([FromBody] UserNewRoleParameters parameters)
        {
            var result = await this._userService.AddToRoleAsync(new UserNewRoleDto(parameters));
            return Ok(result);
        }

        //[HttpPut("[action]")]
        //public async Task<IActionResult> UpdateUserData()
        //{
        //    return Ok("OK");
        //}

        [ProducesResponseType(401)]
        [ProducesResponseType(typeof(object), 403)]
        [ProducesResponseType(typeof(object), 400)]
        [ProducesResponseType(typeof(Response<UserCurrentIFullInfoDto>), 200)]
        [SwaggerOperation(Summary = "Get info about current user")]
        [HttpGet("[action]")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserInfo()
       {
           var refreshToken = Request.Cookies[ConstantKeys.CookieRefreshToken];
            if (refreshToken == null)
            {
                return Unauthorized();
            }

            var response = await this._userService.GetCurrentUserInfoAsync(refreshToken);
            return Ok(response);
        }

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetUsers()
        //{
        //    return Ok("OK");
        //}

        //[HttpGet("[action]")]
        //public async Task<IActionResult> GetUsersWithDetails()
        //{
        //    return Ok("OK");
        //}

        private void SetRefreshTokenInCookie(string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(5),
                IsEssential = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            };
            Response.Cookies.Append(ConstantKeys.CookieRefreshToken, refreshToken, cookieOptions);
        }
    }
}

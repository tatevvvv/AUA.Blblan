using Blblan.Common.Models;
using Blblan.Common.Services;
using Blblan.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blblan.WebApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly TokenService _tokenService;

        public AuthController(IUserService userService, TokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userService.AuthenticateAsync(model);
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            
            var accessToken = _tokenService.CreateToken(user);
            return Ok(new UserResponse{
                Token = accessToken
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            var user = await _userService.RegisterAsync(model);
            if (user == null)
                return BadRequest(new { message = "User already exists" });

            return Ok(new { message = "Registration successful" });
        }
    }

}

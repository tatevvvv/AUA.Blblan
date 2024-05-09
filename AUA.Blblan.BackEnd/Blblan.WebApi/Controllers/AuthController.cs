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
            UserModel? user = null;
            try
            {
                user = await _userService.AuthenticateAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var accessToken = _tokenService.CreateToken(user);

            return Ok(new UserResponse
            {
                Id = user.Id,
                Token = accessToken
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(SignUpModel model)
        {
            UserModel user;

            try
            {
                user = await _userService.RegisterAsync(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(user);
        }
    }

}

using Microsoft.AspNetCore.Mvc;
using TTMS.Domains.Factories;
using TTMS.Models.Auth.Dtos;
using TTMS.Models.User.Dtos;

namespace TTMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthServiceFactory authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user is null)
                return BadRequest("Username already exists.");

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
        {
            var result = await authService.LoginAsync(request);
            if (result is null)
                return BadRequest("Invalid username or password.");

            return Ok(result);
        }
    }
}

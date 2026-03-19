using Microsoft.AspNetCore.Mvc;
using TaskManager.api.Dtos.AuthDto;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Service.Auth_service;

namespace TaskManager.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (result.IsFailure)
                return Unauthorized(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _authService.Register(userCreateDto);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return CreatedAtAction(nameof(Login), result.Value);
        }
    }
}
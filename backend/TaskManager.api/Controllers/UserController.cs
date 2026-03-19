using Microsoft.AspNetCore.Mvc;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Service.User_service;

namespace TaskManager.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userService.GetUsers();
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            var result = await _userService.CreateUser(userCreateDto);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return CreatedAtAction(nameof(GetUsers), result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto userUpdateDto)
        {
            var result = await _userService.UpdateUser(id, userUpdateDto);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return NoContent();
        }
    }
}
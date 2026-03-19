using Microsoft.AspNetCore.Mvc;
using TaskManager.api.Dtos.TaskItemDto;
using TaskManager.api.Service.TaskItem_service;

namespace TaskManager.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemService _taskItemService;

        public TaskItemController(ITaskItemService taskItemService)
        {
            _taskItemService = taskItemService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskItems()
        {
            var result = await _taskItemService.GetTaskItems();
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTaskItemsByUser(int userId)
        {
            var result = await _taskItemService.GetTaskItemsByUser(userId);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskItemById(int id)
        {
            var result = await _taskItemService.GetTaskItemById(id);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTaskItem([FromBody] TaskItemCreateDto taskItemCreateDto)
        {
            var result = await _taskItemService.CreateTaskItem(taskItemCreateDto);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return CreatedAtAction(nameof(GetTaskItemById), new { id = result.Value!.Id }, result.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskItem(int id, [FromBody] TaskItemUpdateDto taskItemUpdateDto)
        {
            var result = await _taskItemService.UpdateTaskItem(id, taskItemUpdateDto);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(int id)
        {
            var result = await _taskItemService.DeleteTaskItem(id);
            if (result.IsFailure)
                return BadRequest(new { errors = result.Errors });
            return NoContent();
        }
    }
}
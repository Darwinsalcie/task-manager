using TaskManager.api.Common;
using TaskManager.api.Dtos.TaskItemDto;

namespace TaskManager.api.Service.TaskItem_service
{
    public interface ITaskItemService
    {
        Task<Result<IEnumerable<TaskItemResponseDto>>> GetTaskItems();
        Task<Result<IEnumerable<TaskItemResponseDto>>> GetTaskItemsByUser(int userId);
        Task<Result<TaskItemResponseDto>> GetTaskItemById(int taskItemId);
        Task<Result<TaskItemResponseDto>> CreateTaskItem(TaskItemCreateDto taskItemCreateDto);
        Task<Result> UpdateTaskItem(int taskItemId, TaskItemUpdateDto taskItemUpdateDto);
        Task<Result> DeleteTaskItem(int taskItemId);
    }
}
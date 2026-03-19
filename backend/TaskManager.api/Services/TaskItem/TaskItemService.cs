using System.Linq.Expressions;
using TaskManager.api.Common;
using TaskManager.api.Data.Repository;
using TaskManager.api.Dtos.TaskItemDto;
using TaskManager.api.Entities;

namespace TaskManager.api.Service.TaskItem_service
{
    public class TaskItemService : ITaskItemService
    {
        private readonly IGenericRepository<TaskItem> _taskItemRepository;
        private readonly IGenericRepository<User> _userRepository;

        public TaskItemService(
            IGenericRepository<TaskItem> taskItemRepository,
            IGenericRepository<User> userRepository)
        {
            _taskItemRepository = taskItemRepository;
            _userRepository = userRepository;
        }

        public async Task<Result<IEnumerable<TaskItemResponseDto>>> GetTaskItems()
        {
            var taskResult = await _taskItemRepository.Get();

            if (taskResult.IsFailure)
                return Result<IEnumerable<TaskItemResponseDto>>.Failure(taskResult.Errors);

            if (!taskResult.Value?.Any() ?? true)
                return Result<IEnumerable<TaskItemResponseDto>>.Failure("No hay tareas registradas");

            var taskList = new List<TaskItemResponseDto>();
            foreach (var task in taskResult.Value!)
                taskList.Add(task.Map());

            return Result<IEnumerable<TaskItemResponseDto>>.Success(taskList);
        }

        public async Task<Result<IEnumerable<TaskItemResponseDto>>> GetTaskItemsByUser(int userId)
        {
            if (userId <= 0)
                return Result<IEnumerable<TaskItemResponseDto>>.Failure("El id debe ser mayor a 0");

            var taskResult = await _taskItemRepository.Get();

            if (taskResult.IsFailure)
                return Result<IEnumerable<TaskItemResponseDto>>.Failure(taskResult.Errors);

            var userTasks = taskResult.Value?.Where(t => t.UserId == userId);

            if (!userTasks?.Any() ?? true)
                return Result<IEnumerable<TaskItemResponseDto>>.Failure("No hay tareas para este usuario");

            var taskList = userTasks!.Select(t => t.Map());
            return Result<IEnumerable<TaskItemResponseDto>>.Success(taskList);
        }

        public async Task<Result<TaskItemResponseDto>> GetTaskItemById(int taskItemId)
        {
            if (taskItemId <= 0)
                return Result<TaskItemResponseDto>.Failure("El id debe ser mayor a 0");

            var taskResult = await _taskItemRepository.GetById(taskItemId);

            if (taskResult.IsFailure)
                return Result<TaskItemResponseDto>.Failure(taskResult.Errors);

            if (taskResult.Value is null)
                return Result<TaskItemResponseDto>.Failure($"No existe una tarea con id {taskItemId}");

            return Result<TaskItemResponseDto>.Success(taskResult.Value.Map());
        }

        public async Task<Result<TaskItemResponseDto>> CreateTaskItem(TaskItemCreateDto taskItemCreateDto)
        {
            if (taskItemCreateDto is null)
                return Result<TaskItemResponseDto>.Failure("El dto es nulo");

            //VALIDACIÓN DE FECHAS
            if (taskItemCreateDto.Start.HasValue && taskItemCreateDto.End.HasValue &&
                taskItemCreateDto.Start >= taskItemCreateDto.End)
            {
                return Result<TaskItemResponseDto>.Failure("La fecha de inicio debe ser menor que la fecha de finalización");
            }

            // Validar que el usuario existe
            var userExists = await _userRepository.ExistsAsync(u => u.Id == taskItemCreateDto.UserId);
            if (userExists.IsFailure)
                return Result<TaskItemResponseDto>.Failure(userExists.Errors);

            if (!userExists.Value)
                return Result<TaskItemResponseDto>.Failure("El usuario no existe");

            var taskCreateResult = await _taskItemRepository.AddAsync(taskItemCreateDto.Map());

            if (taskCreateResult.IsFailure)
                return Result<TaskItemResponseDto>.Failure(taskCreateResult.Errors);

            return Result<TaskItemResponseDto>.Success(taskCreateResult.Value!.Map());
        }

        public async Task<Result> UpdateTaskItem(int taskItemId, TaskItemUpdateDto taskItemUpdateDto)
        {
            if (taskItemUpdateDto is null)
                return Result.Failure("El dto es nulo");

            if (taskItemId <= 0)
                return Result.Failure("El id debe ser mayor a 0");

            var taskResult = await _taskItemRepository.GetById(taskItemId);

            if (taskResult.IsFailure)
                return Result.Failure(taskResult.Errors);

            if (taskResult.Value is null)
                return Result.Failure($"No existe una tarea con id {taskItemId}");

            var existingTask = taskResult.Value;

            // MAPEAR SOBRE LA MISMA INSTANCIA 
            taskItemUpdateDto.MapTo(existingTask);

            // ❗ No necesitas Update() realmente si ya está trackeado,
            // pero lo dejamos por consistencia con tu repo
            var updateResult = await _taskItemRepository.UpdateAsync(existingTask);

            if (updateResult.IsFailure)
                return Result.Failure(updateResult.Errors);

            return Result.Success();
        }

        public async Task<Result> DeleteTaskItem(int taskItemId)
        {
            if (taskItemId <= 0)
                return Result.Failure("El id debe ser mayor a 0");

            var taskResult = await _taskItemRepository.GetById(taskItemId);

            if (taskResult.IsFailure)
                return Result.Failure(taskResult.Errors);

            if (taskResult.Value is null)
                return Result.Failure($"No existe una tarea con id {taskItemId}");

            var deleteResult = await _taskItemRepository.DeleteAsync(taskResult.Value);

            if (deleteResult.IsFailure)
                return Result.Failure(deleteResult.Errors);

            return Result.Success();
        }
    }
}
using TaskManager.api.Dtos.TaskItemDto;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Entities;

namespace TaskManager.api.Common
{
    public static class Mapper
    {
        //This le indica al compilador que el metodo es un metodo
        //que extiende el tipo que le precede, sin this fuera
        //un metodo estatico normal que es instancia por Mapper.ToEntity,
        //pero ahora se llama por UserCreateDto.ToEntity
        public static UserResponseDto Map(this User entity) =>
            new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Email = entity.Email,
                UserRole = entity.UserRole
            };

        public static User Map(this UserCreateDto dto) =>  //bodied expresion
            new()
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                UserRole = dto.UserRole
            };

        public static User Map(this UserUpdateDto dto) =>
            new()
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = dto.Password,
                UserRole = dto.UserRole
            };

        //TaskIem Mapper
        public static TaskItemResponseDto Map(this TaskItem dto) =>
        new()
        {
            Id = dto.Id,
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted= dto.IsCompleted,
            Start = dto.Start,
            End = dto.End,
            UserId = dto.UserId
        };

        public static TaskItem Map(this TaskItemCreateDto dto) =>
        new()
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Start = dto.Start,
            End = dto.End,
            UserId = dto.UserId
        };

        public static TaskItem Map(this TaskItemUpdateDto dto) =>
        new()
        {
            Title = dto.Title,
            Description = dto.Description,
            IsCompleted = dto.IsCompleted,
            Start = dto.Start,
            End = dto.End,
        };

    }
}

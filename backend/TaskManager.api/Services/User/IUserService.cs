using TaskManager.api.Common;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Entities;

namespace TaskManager.api.Service.User_service
{
    public interface IUserService
    {
        //No se exponen las entidades
        public Task<Result<IEnumerable<UserResponseDto>>> GetUsers();
        public Task<Result<UserResponseDto>> CreateUser(UserCreateDto UserCreatedto);

        //El dto de update no tiene id porque no es algo que se deba modificar, pero necesitamos el id
        public Task<Result> UpdateUser(int userId, UserUpdateDto userUpdateDto);
        public Task<Result> DeleteUser(int userId);
    }
}

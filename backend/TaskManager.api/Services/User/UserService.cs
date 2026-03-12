using TaskManager.api.Common;
using TaskManager.api.Data.Repository;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Entities;

namespace TaskManager.api.Service.User_service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        public UserService(IGenericRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        //****************Manejar errores************
        public async Task<Result<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var userResult = await _userRepository.Get();

            var usersList = new List<UserResponseDto>();

            foreach (var user in userResult.Value)
            {
                usersList.Add(user.Map());
            }

            return Result<IEnumerable<UserResponseDto>>.Succes(usersList);
        }
        public Task<Result<UserResponseDto>> CreateUser(UserCreateDto userCreatedto)
        {
            throw new NotImplementedException();


        }
        public Task<Result> DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}

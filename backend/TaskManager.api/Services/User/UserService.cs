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
            //En un get como algunos errores son bloqueantes lo mejor es no seguir ejecutando si ya falló
            //Por eso no acumulamos los errores.
            var userResult = await _userRepository.Get();

            if (userResult.IsFailure)
                return Result<IEnumerable<UserResponseDto>>.Failure(userResult.Errors);


            //Le dice al compilador si el valor es null no intenta acceder a el y evita lanzar un excepción
            if (!userResult.Value?.Any() ?? true)
                return Result<IEnumerable<UserResponseDto>>.Failure("No hay ningun usuario");

            
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

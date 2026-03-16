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

        //GetUsers
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

            //El compilador no entiende que si llegaste al foreach ya validaste que Value no es null.
            //Para suprimirlo se usa el null forgiving operator

            foreach (var user in userResult.Value!)
            {
                usersList.Add(user.Map());
            }

            return Result<IEnumerable<UserResponseDto>>.Success(usersList);

        }

        //CreateUsers
        public async Task<Result<UserResponseDto>> CreateUser(UserCreateDto userCreatedto)
        {
            if (userCreatedto is null)
                return Result<UserResponseDto>.Failure("La entidad que se intenta crear es nula");

            var userCreateResult = await _userRepository.AddAsync(userCreatedto.Map());

            if(userCreateResult.IsFailure)
                return Result<UserResponseDto>.Failure($"Error al guardar la entidad: {userCreateResult.Errors}");


            return Result<UserResponseDto>.Success(userCreateResult.Value!.Map());
        }

        //Update Users
        public async Task<Result> UpdateUser(int userId, UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto is null)
                return Result.Failure("El dto es nulo");

            if (userId <= 0)
                return Result.Failure("El id debe ser mayor o igual a 0");

            var userbyIdResult = await _userRepository.GetById(userId);
            
            if (userbyIdResult.IsFailure)
                return Result.Failure($"Ha habido un error al buscar el usuario: {userbyIdResult.Errors}");

            if (userbyIdResult.Value is null)
                return Result.Failure("El usuario no existe");

            var user = userUpdateDto.Map(userId);

            var userUpdateResult = await _userRepository.UpdateAsync(user);

            if (userUpdateResult.IsFailure)
                return Result.Failure($"Error al actualizar al entidad: {userUpdateResult.Errors}");
            
            return Result.Success();
        }

        public async Task<Result> DeleteUser(int userId)
        {
            if (userId <= 0)
                return Result.Failure("El id debe ser >= 0");

            var user = await _userRepository.GetById(userId);

            if (user.IsFailure)
                return Result.Failure($"Error al obtener el usuario: {user.Errors}");

            if (user.Value is null)
                return Result.Failure($"El usuario para el id {userId} no existe");

            var userDeleteResult = await _userRepository.DeleteAsync(user.Value);

            if (userDeleteResult.IsFailure)
                return Result.Failure($"Error al eliminar el usuario:{userDeleteResult.Errors}");

            return Result.Success();
        }
    }
}

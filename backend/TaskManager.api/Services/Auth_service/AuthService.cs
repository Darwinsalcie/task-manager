using TaskManager.api.Common;
using TaskManager.api.Data.Repository;
using TaskManager.api.Dtos.AuthDto;
using TaskManager.api.Dtos.UserDto;
using TaskManager.api.Entities;

namespace TaskManager.api.Service.Auth_service
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(
            IGenericRepository<User> userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<Result<AuthResponseDto>> Login(LoginDto loginDto)
        {
            if (loginDto is null)
                return Result<AuthResponseDto>.Failure("Los datos de login son nulos");

            // Buscar usuario por email
            var usersResult = await _userRepository.Get();
            if (usersResult.IsFailure)
                return Result<AuthResponseDto>.Failure(usersResult.Errors);

            var user = usersResult.Value?.FirstOrDefault(u => u.Email == loginDto.Email);

            if (user is null)
                return Result<AuthResponseDto>.Failure("Credenciales inválidas");

            // Verificar password con BCrypt
            var isValidPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isValidPassword)
                return Result<AuthResponseDto>.Failure("Credenciales inválidas");

            var token = _tokenService.GenerateToken(user);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.UserRole.ToString(),
                Token = token
            });
        }

        public async Task<Result<AuthResponseDto>> Register(UserCreateDto userCreateDto)
        {
            if (userCreateDto is null)
                return Result<AuthResponseDto>.Failure("Los datos de registro son nulos");

            // Validar unicidad
            var exists = await _userRepository.ExistsAsync(
                u => u.Email == userCreateDto.Email || u.Name == userCreateDto.Name);

            if (exists.IsFailure)
                return Result<AuthResponseDto>.Failure(exists.Errors);

            if (exists.Value)
                return Result<AuthResponseDto>.Failure("El email o nombre ya están registrados");

            // Hashear password antes de guardar
            var userEntity = userCreateDto.Map();
            userEntity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(userCreateDto.Password);

            var createResult = await _userRepository.AddAsync(userEntity);
            if (createResult.IsFailure)
                return Result<AuthResponseDto>.Failure(createResult.Errors);

            var token = _tokenService.GenerateToken(createResult.Value!);

            return Result<AuthResponseDto>.Success(new AuthResponseDto
            {
                Id = createResult.Value!.Id,
                Name = createResult.Value.Name,
                Email = createResult.Value.Email,
                Role = createResult.Value.UserRole.ToString(),
                Token = token
            });
        }
    }
}
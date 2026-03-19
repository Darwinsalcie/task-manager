using TaskManager.api.Common;
using TaskManager.api.Dtos.AuthDto;
using TaskManager.api.Dtos.UserDto;

namespace TaskManager.api.Service.Auth_service
{
    public interface IAuthService
    {
        Task<Result<AuthResponseDto>> Login(LoginDto loginDto);
        Task<Result<AuthResponseDto>> Register(UserCreateDto userCreateDto);
    }
}
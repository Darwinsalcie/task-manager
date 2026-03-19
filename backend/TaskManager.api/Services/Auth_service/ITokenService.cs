using TaskManager.api.Entities;

namespace TaskManager.api.Service.Auth_service
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
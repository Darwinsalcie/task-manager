using TaskManager.api.Service.Auth_service;
using TaskManager.api.Service.TaskItem_service;
using TaskManager.api.Service.User_service;

namespace TaskManager.api.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskItemService, TaskItemService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}
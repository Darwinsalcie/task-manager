using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using TaskManager.api.Entities;

namespace TaskManager.api.Dtos.UserDto
{
    public class UserResposeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role UserRole { get; set; }

    }
}

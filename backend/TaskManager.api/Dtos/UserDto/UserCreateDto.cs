using System.ComponentModel.DataAnnotations;
using TaskManager.api.Entities;

namespace TaskManager.api.Dtos.UserDto
{
    public class UserCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        [EmailAddress]
        //****** Mas adelante debe ser unico *********
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public Role UserRole { get; set; }

    }


}

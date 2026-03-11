using System.ComponentModel.DataAnnotations;

namespace TaskManager.api.Dtos.TaskItemDto
{
    public class TaskItemResponseDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }

        [Required]
        public bool IsCompleted { get; set; } = false;

        //Dejar para cuando se habiliten audit_entities y soft delete
        //public bool IsActive {get; set;}
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }

        //Fk
        [Required]
        public int UserId { get; set; }
    }
}

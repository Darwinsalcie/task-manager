using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.api.Entities
{
    public class User
    {
        public int Id {get; set;}
        
        [Required]
        [MaxLength(100)]
        public string Name {get; set;} = string.Empty;
        
        [Required]
        [MaxLength(150)]
        [EmailAddress]
        //****** Mas adelante debe ser unico *********
        public string Email {get; set;} = string.Empty;

        [Required]
        [MaxLength(255)]
        public string PasswordHash {get; set;} = string.Empty;

        [Required]
        public Role UserRole {get; set;}

        //Navigation Properties
        public ICollection<TaskItem> TaskItems {get; set;} = new List<TaskItem>();
        
    }

    public enum Role : byte
    {
        Admin,
        User
    }
}
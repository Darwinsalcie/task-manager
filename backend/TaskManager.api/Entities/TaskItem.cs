using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.api.Entities
{
    public class TaskItem
    {
        /* podemos forzar a que para crear un TaskItem
        se deba asignar un user primero
        public TaskItem(User user)
        {
            User = user;
        }

        */
        public int Id {get; set;}

        [Required]
        [MaxLength(150)]
        public string Title {get; set;} = string.Empty;

        [Required]
        [MaxLength(255)]
        public string? Description {get; set;}

        [Required]
        public bool IsCompleted {get; set;} = false;

        //Dejar para cuando se habiliten audit_entities y soft delete
        //public bool IsActive {get; set;}
        public DateTime? Start {get; set;}
        public DateTime? End {get; set;}

        //Fk
        [Required]
        public int UserId {get; set;}

        //Navigation Properties
        //null + ! "operador null-fogiving" - indica al compilador  que en tiempo
        // de ejecución user no será nulo.
        //Para usar esto debemos cargar la navegación de forma explícita, es decir, no usar lazy loading.
        public User User {get; set;} = null!;
    }
}
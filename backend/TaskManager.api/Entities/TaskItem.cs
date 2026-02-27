using System;
using System.Collections.Generic;
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
        public string Tittle {get; set;} = string.Empty;
        public string? Description {get; set;}
        public bool IsCompleted {get; set;}
        public bool IsActive {get; set;}
        public DateTime? Start {get; set;}
        public DateTime? End {get; set;}
        
        //Fk
        public int UserId {get; set;}

        //Navigation Properties
        //null + ! "operador null-fogiving" - indica al compilador  que en tiempo
        // de ejecución user no será nulo.
        public User User {get; set;} = null!;
    }
}
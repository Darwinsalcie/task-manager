using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.api.Entities
{
    public class User
    {
        public int Id {get; set;}
        public string Name {get; set;} = string.Empty;
        public string Email {get; set;} = string.Empty;
        public string PasswordHash {get; set;} = string.Empty;
        public Role UserRole {get; set;}

        //Agregar las navigation properties de la lista de tareas
        // Ver opciones IColletion, List, etc
    }

    public enum Role : byte
    {
        Admin,
        User
    }
}
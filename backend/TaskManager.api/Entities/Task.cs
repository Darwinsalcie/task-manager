using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.api.Entities
{
    public class Task
    {
        public int Id {get; set;}
        public string Tittle {get; set;} = string.Empty;
        public string? Description {get; set;}
        public bool IsCompleted {get; set;}
        public bool IsActive {get; set;}
        public DateTime? Start {get; set;}
        public DateTime? End {get; set;}


        //agregar las navigation properties de usuario
    }
}
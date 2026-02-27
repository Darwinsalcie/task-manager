using Microsoft.EntityFrameworkCore;

using TaskManager.api.Entities;
namespace TaskManager.api.Data
{
    public class TaskManagerDbContext : DbContext
    {
        //Recibimos las opciones de configuración a través del constructor
        public TaskManagerDbContext(
            DbContextOptions<TaskManagerDbContext> options
            ) : base(options) 
        {
            
        }

        //Definimos los DbSets

        public DbSet<User> Users {get; set;}
        public DbSet<TaskItem> Tasks {get; set;}

        //Sobre escribimos OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primero, llamamos al método base para que se ejecute la logica del metodo de la clase base
            base.OnModelCreating(modelBuilder);
        }
    }
}
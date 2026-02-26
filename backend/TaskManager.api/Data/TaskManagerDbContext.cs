using Microsoft.EntityFrameworkCore;

namespace TaskManager.api.Data
{
    public class TaskManagerDbContext : DbContext
    {
        public TaskManagerDbContext(
            DbContextOptions<TaskManagerDbContext> options
            ) : base(options) 
        {
            
        }
    }
}
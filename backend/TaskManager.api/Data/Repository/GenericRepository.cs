
using Microsoft.EntityFrameworkCore;

namespace TaskManager.api.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TaskManagerDbContext _dbContext;
        public GenericRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> Get()
            => await _dbContext.Set<T>().ToListAsync();
        public async Task<T?> GetById(int id)
            => await _dbContext.Set<T>().FindAsync(id);
        public async Task<T> AddAsync(T entity)
        {
            //El AddAsync solo es necesario en casos como cuando los generadores de Id
            //tienen que consultar la db
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<bool> UpdateAsync(T entity)
        {
            try {

                _dbContext.Update(entity);
                await _dbContext.SaveChangesAsync();
                return true;
            
            }
            catch (Exception e) {
                return false;
            }
        }
        public async Task<bool> DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }


    }
}

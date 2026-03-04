
using Microsoft.EntityFrameworkCore;
using TaskManager.api.Common;

namespace TaskManager.api.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly TaskManagerDbContext _dbContext;
        public GenericRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Result<IEnumerable<T>>> Get() 
        {
            var entities = await _dbContext.Set<T>().ToListAsync();

            // Retorrna List y hacemos Upcasting para mantener la lista desacoplada
            // Estoy transportando los datos en un objeto List con todo lo que conlleva,
            //pero al usar la interfaz tengo el seguro del contrato que me impide exponer metodos
            //Que me hagan ese objeto incompatible con otros objetos que implementen IEnumerble.
            var resultEntities = Result<IEnumerable<T>>.Succes(entities);
            return resultEntities;
        }
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


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
            try
            {
                var entities = await _dbContext.Set<T>().ToListAsync();

                // Retorrna List y hacemos Upcasting para mantener la lista desacoplada
                // Estoy transportando los datos en un objeto List con todo lo que conlleva,
                //pero al usar la interfaz tengo el seguro del contrato que me impide exponer metodos
                //Que me hagan ese objeto incompatible con otros objetos que implementen IEnumerble.
                var resultEntities = Result<IEnumerable<T>>.Success(entities);
                return resultEntities;
            }
            catch (Exception ex) 
            {
                return Result<IEnumerable<T>>.Failure(ex.Message);
            }

        }
        public async Task<Result<T?>> GetById(int id)
        {
            try
            {
                //Nul es un resultado valido de la operacíón por lo que se maneja
                //en la capa de servicio mandandolo en la lista de errores de validación
                //como un failure.
                var entity = await _dbContext.Set<T>().FindAsync(id);
                var resultEntity = Result<T?>.Success(entity);
                return resultEntity;
            }

            catch (Exception ex) 
            {
                return Result<T?>.Failure(ex.Message);
            }
        }


        //*******Verificar Name and Email unicos
        public async Task<Result<T>> AddAsync(T entity)
        {
            //El AddAsync solo es necesario en casos como cuando los generadores de Id
            //tienen que consultar la db

            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();

                return Result<T>.Success(entity);
            }
            catch(Exception ex)
            {
                return Result<T>.Failure(ex.Message);
            }

        }


        //*******Verificar Name and Email unicos
        public async Task<Result> UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();

                return Result.Success();
            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }

        }
        public async Task<Result> DeleteAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();

                return Result.Success();

            }
            catch(Exception ex)
            {
                return Result.Failure(ex.Message);
            }
        }


    }
}

using TaskManager.api.Common;

namespace TaskManager.api.Data.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        /// <summary>
        /// Devuelve la colección como <see cref="Result{IEnumerable{T}}"/> para exponer 
        /// únicamente la abstracción mínima necesaria para la iteración. 
        /// Esto evita filtrar detalles de implementación (como List o IQueryable), 
        /// reduce el acoplamiento entre capas, mejora la capacidad de prueba 
        /// y permite cambiar la fuente de datos sin afectar las capas superiores.
        /// Como no estoy atado a una implementació  luego si cambio algo en una capa
        /// no se romperán mis capas superiores.
        /// </summary>
        /// <returns>
        /// Una colección de entidades como <see cref="Result{IEnumerable{T}}"/>.
        /// </returns>
        Task<Result<IEnumerable<T>>> Get ();
        Task<Result<T?>> GetById (int id);

        /// <summary>
        /// Se retorna la entidad porque el front no sabe cual es el id que se generó en la db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<Result<T>> AddAsync(T entity);
        
        Task<Result> UpdateAsync (T entity);
        Task<Result> DeleteAsync (T entity);

        //Acá usamos este saveChanges Async para no exponer el contexto que es una capa anterior en la capa de servicios,
        //así si queremos hacer varias operaciones antes de guardar los cambios, lo podemos hacer sin necesidad de exponer el contexto.
        //Task SaveChangesAsync ();

    }
}

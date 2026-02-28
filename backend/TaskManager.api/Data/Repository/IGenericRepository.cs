namespace TaskManager.api.Data.Repository
{
    public interface IGenericRepository<T> where T: class
    {
        /// <summary>
        /// Devuelve la colección como <see cref="IEnumerable{T}"/> para exponer 
        /// únicamente la abstracción mínima necesaria para la iteración. 
        /// Esto evita filtrar detalles de implementación (como List o IQueryable), 
        /// reduce el acoplamiento entre capas, mejora la capacidad de prueba 
        /// y permite cambiar la fuente de datos sin afectar las capas superiores.
        /// Como no estoy atado a una implementació  luego si cambio algo en una capa
        /// no se romperán mis capas superiores.
        /// </summary>
        /// <returns>
        /// Una colección de entidades como <see cref="IEnumerable{T}"/>.
        /// </returns>
        Task<IEnumerable<T>> Get ();
        Task<T?> GetById (int id);
        Task<T> AddAsync(T entity);
        //Solo es marcar el cambio en el change tracker, por lo que no es necesario 
        // retornar nada, ya que el cambio se guardará cuando se llame a SaveChangesAsync.
        Task<bool> UpdateAsync (T entity);
        Task<bool> DeleteAsync (T entity);

        //Acá usamos este saveChanges Async para no exponer el contexto que es una capa anterior en la capa de servicios,
        //así si queremos hacer varias operaciones antes de guardar los cambios, lo podemos hacer sin necesidad de exponer el contexto.
        //Task SaveChangesAsync ();

    }
}

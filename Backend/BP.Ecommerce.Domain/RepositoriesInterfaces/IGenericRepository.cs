namespace BP.Ecommerce.Domain.RepositoriesInterfaces
{
    public interface IGenericRepository<T>
    {
        /// <summary>
        /// Obtiene todos los elementos, ademas permite buscar, ordenar, paginar
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<List<T>> GetAllAsync(string? search, string sort, string order, int limit, int offset);
        /// <summary>
        /// Obtiene elemento por su id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<T> GetByIdAsync(Guid id);
        /// <summary>
        /// Agrega un nuevo elemento
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<T> PostAsync(T item);
        /// <summary>
        /// Actualiza un elemento
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<T> PutAsync(T item);
        /// <summary>
        /// Elimina un elemento de manera logica
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}

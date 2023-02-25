using BP.Ecommerce.Domain.Entities;

namespace BP.Ecommerce.Domain.RepositoryInterfaces
{
    public interface IProductRepository
    {
        /// <summary>
        /// Obtiene el queryable de productos, ademas permite buscar, ordenar y paginar
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> GetQueryable(string search, int limit, int offset, string sort, string order);
        /// <summary>
        /// Obtiene el queryable de producto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> GetQueryableByIdAsync(Guid id);
        /// <summary>
        /// Agrega un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> PostAsync(Product product);
        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Task<IQueryable<Product>> PutAsync(Product product);
        /// <summary>
        /// Elimina de manera logica un producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(Guid id);
    }
}

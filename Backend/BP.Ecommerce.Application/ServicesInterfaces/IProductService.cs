using BP.Ecommerce.Application.Dtos;

namespace BP.Ecommerce.Application.ServicesInterfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Obtiene productos, ademas permite buscar, ordenar y paginar
        /// </summary>
        /// <param name="search"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<List<ProductDto>> GetAllAsync(string search, int limit, int offset, string sort, string order);
        /// <summary>
        /// Obtiene el producto por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDto> GetByIdAsync(Guid id);
        /// <summary>
        /// Agrega un producto
        /// </summary>
        /// <param name="createProductDto"></param>
        /// <returns></returns>
        Task<ProductDto> PostAsync(CreateProductDto createProductDto);
        /// <summary>
        /// Actualiza un producto
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        Task<ProductDto> PutAsync(ProductDto productDto);
        /// <summary>
        /// Elimina de manera logica un producto por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdAsync(Guid id);

    }
}

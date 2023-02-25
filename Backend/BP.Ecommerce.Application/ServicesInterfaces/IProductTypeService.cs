using BP.Ecommerce.Application.DTOs;

namespace BP.Ecommerce.Application.ServicesInterfaces
{
    public interface IProductTypeService
    {
        /// <summary>
        /// Obtiene todos los metodos de entrega, ademas permite buscar, ordenar y paginar
        /// </summary>
        /// <param name="search"></param>
        /// <param name="sort"></param>
        /// <param name="order"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<List<ProductTypeDto>> GetAllAsync(string? search, string sort, string order, int limit, int offset);
        /// <summary>
        /// Obtiene metodo de entrega por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ProductTypeDto> GetByIdAsync(Guid id);
        /// <summary>
        /// Crea nuevo metodo de entrega
        /// </summary>
        /// <param name="createProductTypeDto"></param>
        /// <returns></returns>
        public Task<ProductTypeDto> PostAsync(CreateProductTypeDto createProductTypeDto);
        /// <summary>
        /// Actualiza metodo de entrega
        /// </summary>
        /// <param name="productTypeDto"></param>
        /// <returns></returns>
        public Task<ProductTypeDto> PutAsync(ProductTypeDto productTypeDto);
        /// <summary>
        /// Elimina metodo de entrega
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}

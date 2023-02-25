using BP.Ecommerce.Application.DTOs;

namespace BP.Ecommerce.Application.ServicesInterfaces
{
    public interface IDeliveryMethodService
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
        public Task<List<DeliveryMethodDto>> GetAllAsync(string? search, string sort, string order, int limit, int offset);
        /// <summary>
        /// Obtiene metodo de entrega por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<DeliveryMethodDto> GetByIdAsync(Guid id);
        /// <summary>
        /// Agrega nuevo metodo de entrega
        /// </summary>
        /// <param name="createDeliveryMethodDto"></param>
        /// <returns></returns>
        public Task<DeliveryMethodDto> PostAsync(CreateDeliveryMethodDto createDeliveryMethodDto);
        /// <summary>
        /// Actualiza metodo de entrega
        /// </summary>
        /// <param name="deliveryMethodDto"></param>
        /// <returns></returns>
        public Task<DeliveryMethodDto> PutAsync(DeliveryMethodDto deliveryMethodDto);
        /// <summary>
        /// Elimina metodo de entrega por id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<bool> DeleteAsync(Guid id);
    }
}

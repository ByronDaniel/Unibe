using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Domain.Entities;

namespace BP.Ecommerce.Application.ServicesInterfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Obtiene todas las ordenes, ademas permite buscar por estado, ordenar, paginar
        /// </summary>
        /// <param name="state"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public Task<List<OrderGetAllDto>> GetAllAsync(string? state, string order, string sort, int limit, int offset);
        /// <summary>
        /// Crea una nueva orden
        /// </summary>
        /// <returns></returns>
        public Task<OrderNewDto> CreateOrderAsync();
        /// <summary>
        /// Agrega un producto a la orden, si ya existe solo aumenta la cantidad
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="addProductDto"></param>
        /// <returns></returns>
        public Task<OrderProductDto> AddProductAsync(Guid orderId, AddProductDto addProductDto);
        /// <summary>
        /// Actualiza la cantidad de un producto de la orden
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public Task<OrderProductDto> UpdateProductAsync(Guid orderId, UpdateOrderProductDto orderProduct);
        /// <summary>
        /// Elimina un producto de la orden
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<bool> RemoveProductAsync(Guid orderId, Guid productId);
        /// <summary>
        /// Paga una orden, cambia el estado de la orden a pagado y devuelve valor total con su valor de entrega
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<OrderDto> GetByIdAsync(Guid orderId);
        /// <summary>
        /// Paga una orden, cambia el estado de la orden a pagado y devuelve valor total con su valor de entrega
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <returns></returns>
        public Task<OrderDto> PayAsync(Guid orderId, Guid deliveryMethodId);
        /// <summary>
        /// Cancela la orden si todavia no esta pagada, si esta pagada cambia al estado reembolsado, ademas devuelve productos al stock
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<OrderDto> CancelAsync(Guid orderId);

        public Task<int> GetProductsQuantity(Guid orderId);
        
    }
}

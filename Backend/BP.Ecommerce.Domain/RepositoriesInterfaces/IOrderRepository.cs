using BP.Ecommerce.Domain.Entities;

namespace BP.Ecommerce.Domain.RepositoriesInterfaces
{
    public interface IOrderRepository
    {
        /// <summary>
        /// Crea una nueva orden
        /// </summary>
        /// <returns></returns>
        public Task<Order> CreateOrderAsync();
        /// <summary>
        /// Agrega un producto a la orden, si ya existe solo aumenta la cantidad
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public Task<OrderProduct> AddProductAsync(Guid orderId, OrderProduct orderProduct);
        /// <summary>
        /// Actualiza la cantidad de un producto de la orden
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderProduct"></param>
        /// <returns></returns>
        public Task<OrderProduct> UpdateProductAsync(Guid orderId, OrderProduct orderProduct);
        /// <summary>
        /// Elimina un producto de la orden
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public Task<bool> RemoveProductAsync(Guid orderId, Guid productId);
        /// <summary>
        /// Obtiene todas las ordenes, ademas permite buscar por estado, ordenar, paginar
        /// </summary>
        /// <param name="state"></param>
        /// <param name="order"></param>
        /// <param name="sort"></param>
        /// <param name="limit"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public IQueryable<Order> GetAllAsync(string state, string order, string sort, int limit, int offset);
        /// <summary>
        /// Obtiene una orden por su id
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public IQueryable<Order> GetOrderByIdAsync(Guid orderId);
        /// <summary>
        /// Paga una orden, cambia el estado de la orden a pagado y devuelve valor total con su valor de entrega
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="deliveryMethodId"></param>
        /// <returns></returns>
        public Task<IQueryable<Order>> PayAsync(Guid orderId, Guid deliveryMethodId);
        /// <summary>
        /// Cancela la orden si todavia no esta pagada, si esta pagada cambia al estado reembolsado, ademas devuelve productos al stock
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public Task<IQueryable<Order>> CancelAsync(Guid orderId);
        /// <summary>
        /// Actualiza la orden
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="subtotal"></param>
        /// <param name="totalPrice"></param>
        /// <returns></returns>
        public Task<Order> UpdateOrderAsync(Guid orderId, decimal subtotal, decimal totalPrice);

        public Task<int> GetProductsQuantity(Guid orderId);

    }
}

using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoriesInterfaces;
using BP.Ecommerce.Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BP.Ecommerce.Infraestructure.RepositoriesImplementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly EcommerceDbContext context;

        public OrderRepository(EcommerceDbContext context)
        {
            this.context = context;
        }

        public async Task<Order> CreateOrderAsync()
        {
            Order order = new Order()
            {
                State = Status.Pendiente.ToString(),
                Subtotal = 0,
                TotalPrice = 0
            };
            await context.Order.AddAsync(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<OrderProduct> AddProductAsync(Guid orderId, OrderProduct orderProduct)
        {
            Order orderFind = await context.Order.Where(o => o.Id == orderId && o.State == Status.Pendiente.ToString()).SingleOrDefaultAsync();
            if (orderFind == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            Product product = await ValidateQuantityStock(orderProduct);
            OrderProduct orderProductExist = await context.OrderProducts.Where(o => o.OrderId == orderId && o.ProductId == product.Id).SingleOrDefaultAsync();

            orderProduct.OrderId = orderId;
            if (orderProductExist == null)
            {
                orderProduct.Total = product.Price * orderProduct.ProductQuantity;
                await context.OrderProducts.AddAsync(orderProduct);
                await context.SaveChangesAsync();
            }
            else
            {
                orderProductExist.ProductQuantity += orderProduct.ProductQuantity;
                orderProductExist.Total += product.Price * orderProduct.ProductQuantity;
                context.Entry(orderProductExist).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            product.Stock -= orderProduct.ProductQuantity;
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            if (orderProductExist == null)
            {
                return orderProduct;
            }
            else
            {
                return orderProductExist;
            }
        }

        public async Task<OrderProduct> UpdateProductAsync(Guid orderId, OrderProduct orderProduct)
        {
            Order orderFind = await context.Order.Where(o => o.Id == orderId && o.State == Status.Pendiente.ToString()).SingleOrDefaultAsync();
            if (orderFind == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            OrderProduct orderProductFind = await context.OrderProducts.Where(o => o.ProductId == orderProduct.ProductId && o.OrderId == orderId).SingleOrDefaultAsync();
            Product product = await ValidateQuantityStock(orderProduct, 'U');

            var stockRestado = (orderProduct.ProductQuantity - orderProductFind.ProductQuantity);
            var stockAumentado = (orderProductFind.ProductQuantity - orderProduct.ProductQuantity);
            if (orderProductFind.ProductQuantity < orderProduct.ProductQuantity)
            {
                if (product.Stock - stockRestado < 0)
                    throw new ArgumentException($"No hay suficiente stock, unidades disponibles: {product.Stock}");

                product.Stock -= stockRestado;
            }
            else
            {
                if (orderProductFind.ProductQuantity < orderProduct.ProductQuantity)
                    throw new ArgumentException($"No se permite devolver mas productos de los adquiridos, Productos adquiridos {orderProductFind.ProductQuantity}, productos devueltos {orderProduct.ProductQuantity}");

                product.Stock += stockAumentado;
            }
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            orderProductFind.ProductQuantity = orderProduct.ProductQuantity;
            orderProductFind.Total = product.Price * orderProductFind.ProductQuantity;
            context.Entry(orderProductFind).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return orderProductFind;
        }

        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            Order orderFind = await context.Order.Where(o => o.Id == orderId && o.State == Status.Pendiente.ToString()).SingleOrDefaultAsync();
            if (orderFind == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            OrderProduct orderProduct = await context.OrderProducts.Where(o => o.OrderId == orderId && o.ProductId == productId).SingleOrDefaultAsync();
            if (orderProduct == null)
                throw new ArgumentException($"No existe la orden {orderId} con el producto {productId}");
            context.OrderProducts.Remove(orderProduct);
            await context.SaveChangesAsync();

            var quantity = orderProduct.ProductQuantity;
            Product product = await context.Products.FindAsync(productId);
            if (product == null)
                throw new ArgumentException($"No existe el producto {productId}");
            product.Stock += quantity;
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return true;
        }
        public IQueryable<Order> GetAllAsync(string state,string order, string sort, int limit, int offset)
        {
            var query = context.Order.AsQueryable();

            if (!string.IsNullOrEmpty(state))
            { 
                query = from myOrder in query
                    where myOrder.State.ToUpper().Contains(state.ToUpper())
                    select myOrder;
            }

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort.ToUpper())
                {
                    case "STATE":
                        query = order.ToUpper() == "ASC"
                            ? query.OrderBy(t => t.State)
                            : order.ToUpper() == "DESC"
                                ? query.OrderByDescending(t => t.State)
                                : throw new ArgumentException($"Argumento: {order} no valido");
                        break;
                    default:
                        throw new ArgumentException($"Argumento: {sort} no valido");
                }
            }

            query = query.Skip(offset).Take(limit);
            return query;

        }
        public IQueryable<Order> GetOrderByIdAsync(Guid orderId)
        {
            return context.Order.Where(o => o.Id == orderId).AsQueryable();
        }

        public async Task<IQueryable<Order>> PayAsync(Guid orderId, Guid deliveryMethodId)
        {
            IQueryable<Order> query = context.Order.Where(o => o.Id == orderId && o.State == Status.Pendiente.ToString()).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");
            DeliveryMethod deliveryMethod = await context.DeliveryMethods.FindAsync(deliveryMethodId);
            if (deliveryMethod == null)
                throw new ArgumentException($"No existe el metodo de entrega con id: {deliveryMethod}");
            
            order.DeliveryMethodId = deliveryMethodId;
            order.State = Status.Pagado.ToString();
            order.DateModification = DateTime.Now;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return query;
        }

        public async Task<IQueryable<Order>> CancelAsync(Guid orderId)
        {

            var query = context.Order.Where(o => o.Id == orderId && (o.State == Status.Pagado.ToString() || o.State == Status.Pendiente.ToString())).AsQueryable();
            Order order = await query.SingleOrDefaultAsync();
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            List<OrderProduct> orderProducts = await context.OrderProducts.Where(o => o.OrderId == orderId).ToListAsync();
            Product productSelected = new Product();
            foreach (var orderProduct in orderProducts)
            {
                productSelected = await context.Products.Where(o => o.Id == orderProduct.ProductId).SingleAsync();
                productSelected.Stock += orderProduct.ProductQuantity;
                context.Entry(productSelected).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }

            if (order.State == Status.Pendiente.ToString())
            {
                order.State = Status.Cancelado.ToString();
            }
            else if (order.State == Status.Pagado.ToString())
            {
                order.State = Status.Reembolsado.ToString();
            }
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return query;
        }

        public async Task<Product> ValidateQuantityStock(OrderProduct orderProduct, char operation = 'C')
        {
            Product product = await context.Products.FindAsync(orderProduct.ProductId);
            if (product == null)
                throw new ArgumentException($"No existe el producto con Id: {orderProduct.ProductId}");

            if (orderProduct.ProductQuantity <= 0)
                throw new ArgumentException("La cantidad seleccionada debe ser mayor a 0");

            if (operation != 'U')
            {
                if (product.Stock < orderProduct.ProductQuantity || product.Stock == 0)
                    throw new ArgumentException($"No hay suficiente stock, unidades disponibles: {product.Stock}");
            }

            return product;
        }

        public async Task<Order> UpdateOrderAsync(Guid orderId, decimal subtotal, decimal totalPrice)
        {
            Order order = await context.Order.FindAsync(orderId);
            if (order == null)
                throw new ArgumentException($"No existe la orden con id: {orderId}");

            order.Subtotal = subtotal;
            order.TotalPrice = totalPrice;
            context.Entry(order).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<int> GetProductsQuantity(Guid orderId)
        {
            return this.context.OrderProducts.Where(e => e.OrderId == orderId).Count();
        }
    }
}

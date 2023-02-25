using AutoMapper;
using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.Exceptions;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoriesInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BP.Ecommerce.Application.ServicesImplementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository repository;
        private readonly IMapper mapper;

        public OrderService(IOrderRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<OrderNewDto> CreateOrderAsync()
        {
            var order = await repository.CreateOrderAsync();
            return mapper.Map<OrderNewDto>(order);
        }

        public async Task<OrderProductDto> AddProductAsync(Guid orderId, AddProductDto addProductDto)
        {
            var orderProduct = mapper.Map<OrderProduct>(addProductDto);
            var orderProductDto = mapper.Map<OrderProductDto>(await repository.AddProductAsync(orderId, orderProduct));
            return orderProductDto;
        }

        public async Task<OrderProductDto> UpdateProductAsync(Guid orderId, UpdateOrderProductDto orderProductDto)
        {
            var orderProduct = mapper.Map<OrderProduct>(orderProductDto);
            var orderProductResultDto = mapper.Map<OrderProductDto>(await repository.UpdateProductAsync(orderId, orderProduct));
            return orderProductResultDto;
        }

        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await repository.RemoveProductAsync(orderId, productId);
        }

        public async Task<List<OrderGetAllDto>> GetAllAsync(string? state, string order, string sort, int limit, int offset)
        {
            var query = repository.GetAllAsync(state, order, sort, limit, offset);

            var orderDto = await query.Select(o => new OrderGetAllDto()
            {
                Id = o.Id,
                DeliveryMethod = o.DeliveryMethod.Name,
                orderProducts = o.orderProducts.Select(op => new OrderProductResultDto() { Product = op.Product.Name, Price = op.Product.Price, ProductQuantity = op.ProductQuantity, Total = op.Total }).ToList(),
                State = o.State,
                DeliveryMethodPrice = o.DeliveryMethod.PriceByKm,
                Subtotal = o.Subtotal,
                TotalPrice = o.TotalPrice
            }).ToListAsync();
            return orderDto;
        }

        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            decimal total = 0;
            var query = repository.GetOrderByIdAsync(orderId);
            if (await query.SingleOrDefaultAsync() == null)
                throw new NotFoundException($"No existe la orden con id: {orderId}");

            OrderDto orderDto = await query.Select(o => new OrderDto()
            {
                Id = o.Id,
                DeliveryMethod = o.DeliveryMethod.Name,
                orderProducts = o.orderProducts.Select(op => new OrderProductResultDto() {ProductId = op.Product.Id, Product = op.Product.Name, ImageUrl = op.Product.ImageUrl, Price = op.Product.Price, Stock = op.Product.Stock, ProductQuantity = op.ProductQuantity, Total = op.Total }).ToList(),
                State = o.State,
                DeliveryMethodPrice = o.DeliveryMethod.PriceByKm != null? o.DeliveryMethod.PriceByKm: 0,
                Subtotal = o.Subtotal,
                TotalPrice = o.TotalPrice
            }).SingleOrDefaultAsync();

            foreach (var product in orderDto.orderProducts)
            {
                total += product.Total;
            }

            orderDto.Subtotal = total - (total * (decimal)0.12);
            orderDto.Iva = (total * (decimal)0.12);
            orderDto.TotalPrice = total +(decimal) orderDto.DeliveryMethodPrice;
            return orderDto;
        }

        public async Task<OrderDto> PayAsync(Guid orderId, Guid deliveryMethodId)
        {
            await repository.PayAsync(orderId, deliveryMethodId);
            var orderDto = await GetByIdAsync(orderId);
            await repository.UpdateOrderAsync(orderId, orderDto.Subtotal, orderDto.TotalPrice);
            return orderDto;
        }

        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            await repository.CancelAsync(orderId);
            return await GetByIdAsync(orderId);
        }

        public async Task<int> GetProductsQuantity(Guid orderId)
        {
            return await this.repository.GetProductsQuantity(orderId);
        }
    }
}

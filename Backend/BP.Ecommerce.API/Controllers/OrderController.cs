using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Ecommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService service;

        public OrderController(IOrderService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<OrderGetAllDto>> GetAllAsync(string? state, string order="asc", string sort = "state", int limit = 0, int offset = 0)
        {
            return await service.GetAllAsync(state, order, sort, limit, offset);
        }

        [HttpPost]
        public async Task<OrderNewDto> CreateOrderAsync()
        {
            return await service.CreateOrderAsync();
        }

        [HttpPost("{orderId}/Product")]
        public async Task<OrderProductDto> AddProductAsync(Guid orderId, AddProductDto addProductDto)
        {
            return await service.AddProductAsync(orderId, addProductDto);
        }

        [HttpPut("{orderId}/Product")]
        public async Task<OrderProductDto> UpdateProductAsync(Guid orderId, UpdateOrderProductDto orderProduct)
        {
            return await service.UpdateProductAsync(orderId, orderProduct);
        }

        [HttpDelete("{orderId}/Product/{productId}")]
        public async Task<bool> RemoveProductAsync(Guid orderId, Guid productId)
        {
            return await service.RemoveProductAsync(orderId, productId);
        }

        [HttpGet("Show/{orderId}")]
        public async Task<OrderDto> GetByIdAsync(Guid orderId)
        {
            return await service.GetByIdAsync(orderId);
        }

        [HttpGet("Quantity-products")]
        public async Task<int> GetProductQuantityAsync(Guid orderId)
        {
            return await service.GetProductsQuantity(orderId);
        }

        [HttpPut("Pay/{orderId}/DeliveryMethod/{deliveryMethodId}")]
        public async Task<OrderDto> PayAsync(Guid orderId, Guid deliveryMethodId)
        {
            return await service.PayAsync(orderId, deliveryMethodId);
        }

        [HttpPut("Cancel/{orderId}")]
        public async Task<OrderDto> CancelAsync(Guid orderId)
        {
            return await service.CancelAsync(orderId);
        }
    }
}

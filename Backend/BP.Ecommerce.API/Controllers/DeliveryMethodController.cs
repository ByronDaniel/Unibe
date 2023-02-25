using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Ecommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryMethodController : ControllerBase
    {
        private readonly IDeliveryMethodService service;

        public DeliveryMethodController(IDeliveryMethodService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<DeliveryMethodDto>> GetAllAsync(string? search, string sort = "Name", string order="Asc", int limit=0, int offset=0)
        {
            return await service.GetAllAsync(search, sort, order, limit, offset);
        }

        [HttpGet("{id}")]
        public async Task<DeliveryMethodDto> GetByIdAsync(Guid id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<DeliveryMethodDto> PostAsync(CreateDeliveryMethodDto createDeliveryMethodDto)
        {
            return await service.PostAsync(createDeliveryMethodDto);
        }

        [HttpPut]
        public async Task<DeliveryMethodDto> PutAsync(DeliveryMethodDto deliveryMethodDto)
        {
            return await service.PutAsync(deliveryMethodDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await service.DeleteAsync(id);
        }
    }
}

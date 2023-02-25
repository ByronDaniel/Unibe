using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Ecommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeService service;

        public ProductTypeController(IProductTypeService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<ProductTypeDto>> GetAllAsync(string? search, string sort="Name", string order="Asc", int limit=0, int offset=0)
        {
            return await service.GetAllAsync(search, sort, order, limit, offset);
        }

        [HttpGet("{id}")]
        public async Task<ProductTypeDto> GetByIdAsync(Guid id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductTypeDto> PostAsync(CreateProductTypeDto createProductTypeDto)
        {
            return await service.PostAsync(createProductTypeDto);
        }

        [HttpPut]
        public async Task<ProductTypeDto> PutAsync(ProductTypeDto brandDto)
        {
            return await service.PutAsync(brandDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteAsync(Guid id)
        {
            return await service.DeleteAsync(id);
        }
    }
}

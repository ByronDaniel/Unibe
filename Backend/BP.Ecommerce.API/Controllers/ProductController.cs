using BP.Ecommerce.Application.Dtos;
using BP.Ecommerce.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Ecommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<List<ProductDto>> GetAllAsync(string? search, string sort = "Name", string order = "Asc",int limit = 0, int offset = 0)
        {
            return await _service.GetAllAsync(search, limit, offset, sort, order);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            return await _service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<ProductDto> PostAsync(CreateProductDto createProductDto)
        {
            return await _service.PostAsync(createProductDto);
        }

        [HttpPut]
        public async Task<ProductDto> PutAsync(ProductDto productDto)
        {
            return await _service.PutAsync(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _service.DeleteByIdAsync(id);
        }
    }
}

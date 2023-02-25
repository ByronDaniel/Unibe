using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BP.Ecommerce.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService service;

        public BrandController(IBrandService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<List<BrandDto>> GetAllAsync(string? search, string sort = "Name", string order="Asc", int limit=0, int offset=0)
        {
            return await service.GetAllAsync(search, sort, order, limit, offset);
        }

        [HttpGet("{id}")]
        public async Task<BrandDto> GetByIdAsync(Guid id)
        {
            return await service.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<BrandDto> PostAsync(CreateBrandDto createBrandDto)
        {
            return await service.PostAsync(createBrandDto);
        }

        [HttpPut]
        public async Task<BrandDto> PutAsync(BrandDto brandDto)
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

using AutoMapper;
using BP.Ecommerce.Application.Dtos;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BP.Ecommerce.Application.ServicesImplementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllAsync(string search, int limit, int offset, string sort, string order)
        {
            var query = await _repository.GetQueryable(search, limit, offset, sort, order);
            return await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                BrandId = p.BrandId,
                Brand = p.Brand.Name,
                ProductTypeId = p.ProductTypeId,
            }).ToListAsync();
        }

        public async Task<ProductDto> GetByIdAsync(Guid id)
        {
            var query = await _repository.GetQueryableByIdAsync(id);
            return await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                BrandId = p.BrandId,
                Brand = p.Brand.Name,
                ProductTypeId = p.ProductTypeId,
            }).SingleOrDefaultAsync();
        }

        public async Task<ProductDto> PostAsync(CreateProductDto createProductDto)
        {
            var query = await _repository.PostAsync(_mapper.Map<Product>(createProductDto));
            return await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                BrandId = p.BrandId,
                Brand = p.Brand.Name,
                ProductTypeId = p.ProductTypeId,
            }).SingleOrDefaultAsync();
        }

        public async Task<ProductDto> PutAsync(ProductDto productDto)
        {
            var query = await _repository.PutAsync(_mapper.Map<Product>(productDto));
            return await query.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Stock = p.Stock,
                Price = p.Price,
                BrandId = p.BrandId,
                Brand = p.Brand.Name,
                ProductTypeId = p.ProductTypeId,
            }).SingleOrDefaultAsync();
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            return await _repository.DeleteByIdAsync(id);
        }
    }
}

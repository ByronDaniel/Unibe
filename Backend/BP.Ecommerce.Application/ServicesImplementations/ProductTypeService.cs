using AutoMapper;
using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.Exceptions;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoriesInterfaces;
using FluentValidation;

namespace BP.Ecommerce.Application.ServicesImplementations
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly IGenericRepository<ProductType> repository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateProductTypeDto> validatorCreate;
        private readonly IValidator<ProductTypeDto> validatorUpdate;
        
        public ProductTypeService(IGenericRepository<ProductType> repository, IMapper mapper, IValidator<CreateProductTypeDto> validatorCreate, IValidator<ProductTypeDto> validatorUpdate)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validatorCreate = validatorCreate;
            this.validatorUpdate = validatorUpdate;
        }

        public async Task<List<ProductTypeDto>> GetAllAsync(string? search, string sort, string order, int limit, int offset)
        {
            List<ProductType> productTypes = await repository.GetAllAsync(search, sort, order, limit, offset);
            return mapper.Map<List<ProductTypeDto>>(productTypes);
        }

        public async Task<ProductTypeDto> GetByIdAsync(Guid id)
        {
            ProductType productType = await repository.GetByIdAsync(id);
            if (productType == null)
                throw new NotFoundException($"No existe el registro con id: {id}");

            return mapper.Map<ProductTypeDto>(productType);
        }

        public async Task<ProductTypeDto> PostAsync(CreateProductTypeDto createProductTypeDto)
        {
            await validatorCreate.ValidateAndThrowAsync(createProductTypeDto);

            ProductType productType = mapper.Map<ProductType>(createProductTypeDto);
            ProductType productTypeResult = await repository.PostAsync(productType);
            return mapper.Map<ProductTypeDto>(productTypeResult);
        }

        public async Task<ProductTypeDto> PutAsync(ProductTypeDto updateProductTypeDto)
        {
            await validatorUpdate.ValidateAndThrowAsync(updateProductTypeDto);

            ProductType productType = mapper.Map<ProductType>(updateProductTypeDto);
            ProductType productTypeResult = await repository.PutAsync(productType);
            return mapper.Map<ProductTypeDto>(productTypeResult);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}

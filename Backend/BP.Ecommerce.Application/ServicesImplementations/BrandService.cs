using AutoMapper;
using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.Exceptions;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoriesInterfaces;
using FluentValidation;

namespace BP.Ecommerce.Application.ServicesImplementations
{
    public class BrandService : IBrandService
    {
        private readonly IGenericRepository<Brand> repository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateBrandDto> validator;
        private readonly IValidator<BrandDto> validatorBrandDto;

        public BrandService(IGenericRepository<Brand> repository, IMapper mapper, IValidator<CreateBrandDto> validator, IValidator<BrandDto> validatorBrandDto)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
            this.validatorBrandDto = validatorBrandDto;
        }

        public async Task<List<BrandDto>> GetAllAsync(string? search, string sort, string order, int limit, int offset)
        {
            List<Brand> brands = await repository.GetAllAsync(search, sort, order, limit, offset);
            return mapper.Map<List<BrandDto>>(brands);
        }

        public async Task<BrandDto> GetByIdAsync(Guid id)
        {
            Brand brand = await repository.GetByIdAsync(id);
            if (brand == null)
                throw new NotFoundException($"No existe el registro con id: {id}");

            return mapper.Map<BrandDto>(brand);
        }

        public async Task<BrandDto> PostAsync(CreateBrandDto createBrandDto)
        {
            await validator.ValidateAndThrowAsync(createBrandDto);

            Brand brand = mapper.Map<Brand>(createBrandDto);
            Brand brandResult = await repository.PostAsync(brand);
            return mapper.Map<BrandDto>(brandResult);
        }

        public async Task<BrandDto> PutAsync(BrandDto brandDto)
        {
            await validatorBrandDto.ValidateAndThrowAsync(brandDto);
            Brand brand = mapper.Map<Brand>(brandDto);
            Brand brandResult = await repository.PutAsync(brand);
            return mapper.Map<BrandDto>(brandResult);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}

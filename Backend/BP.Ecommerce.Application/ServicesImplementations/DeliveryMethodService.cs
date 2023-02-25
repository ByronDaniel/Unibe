using AutoMapper;
using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Application.Exceptions;
using BP.Ecommerce.Application.ServicesInterfaces;
using BP.Ecommerce.Domain.Entities;
using BP.Ecommerce.Domain.RepositoriesInterfaces;
using FluentValidation;

namespace BP.Ecommerce.Application.ServicesImplementations
{
    public class DeliveryMethodService : IDeliveryMethodService
    {
        private readonly IGenericRepository<DeliveryMethod> repository;
        private readonly IMapper mapper;
        private readonly IValidator<CreateDeliveryMethodDto> validator;
        private readonly IValidator<DeliveryMethodDto> validatorDeliveryMethodDto;

        public DeliveryMethodService(IGenericRepository<DeliveryMethod> repository, IMapper mapper, IValidator<CreateDeliveryMethodDto> validator, IValidator<DeliveryMethodDto> validatorDeliveryMethodDto)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.validator = validator;
            this.validatorDeliveryMethodDto = validatorDeliveryMethodDto;
        }

        public async Task<List<DeliveryMethodDto>> GetAllAsync(string? search, string sort, string order, int limit, int offset)
        {
            List<DeliveryMethod> deliveryMethods = await repository.GetAllAsync(search, sort, order, limit, offset);
            return mapper.Map<List<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<DeliveryMethodDto> GetByIdAsync(Guid id)
        {
            DeliveryMethod deliveryMethod = await repository.GetByIdAsync(id);
            if (deliveryMethod == null)
                throw new NotFoundException($"No existe el registro con id: {id}");

            return mapper.Map<DeliveryMethodDto>(deliveryMethod);
        }

        public async Task<DeliveryMethodDto> PostAsync(CreateDeliveryMethodDto createDeliveryMethodDto)
        {
            await validator.ValidateAndThrowAsync(createDeliveryMethodDto);

            DeliveryMethod deliveryMethod = mapper.Map<DeliveryMethod>(createDeliveryMethodDto);
            DeliveryMethod deliveryMethodResult = await repository.PostAsync(deliveryMethod);
            return mapper.Map<DeliveryMethodDto>(deliveryMethodResult);
        }

        public async Task<DeliveryMethodDto> PutAsync(DeliveryMethodDto deliveryMethodDto)
        {
            await validatorDeliveryMethodDto.ValidateAndThrowAsync(deliveryMethodDto);
            DeliveryMethod deliveryMethod = mapper.Map<DeliveryMethod>(deliveryMethodDto);
            DeliveryMethod deliveryMethodResult = await repository.PutAsync(deliveryMethod);
            return mapper.Map<DeliveryMethodDto>(deliveryMethodResult);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await repository.DeleteAsync(id);
        }
    }
}

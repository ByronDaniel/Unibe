using AutoMapper;
using BP.Ecommerce.Application.Dtos;
using BP.Ecommerce.Application.DTOs;
using BP.Ecommerce.Domain.Entities;

namespace BP.Ecommerce.API.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<CreateBrandDto, Brand>();
            CreateMap<CreateDeliveryMethodDto, DeliveryMethod>();
            CreateMap<DeliveryMethod, DeliveryMethodDto>();
            CreateMap<DeliveryMethodDto, DeliveryMethod>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<ProductTypeDto, ProductType>();
            CreateMap<CreateProductTypeDto, ProductType>();
            CreateMap<OrderProduct, OrderProductDto>();
            CreateMap<OrderProductDto, OrderProduct>();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderNewDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<Order, OrderGetAllDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<AddProductDto, OrderProduct>();
            CreateMap<UpdateOrderProductDto, OrderProduct>();
            CreateMap<ProductDto, Product>();


        }
    }
}

using AutoMapper;
using Product.API.Entities;
using Shared.DTOs.Product;
using Infrastructure.Mapping;

namespace Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatelogProduct, ProductDTO>();
            CreateMap<CreateProductDTO, CatelogProduct>();
            CreateMap<UpdateProductDTO, CatelogProduct>().IgnoreAllNonSetting();
        }
    }
}

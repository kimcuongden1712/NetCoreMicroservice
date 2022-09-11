using AutoMapper;
using Infrastructure.Mappings;
using Product.API.Entities;
using Shared.DTOs.Product;

namespace Product.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogProduct, ProductDTO>();
            CreateMap<CreateProductDTO, CatalogProduct>();
            CreateMap<UpdateProductDTO, CatalogProduct>().IgnoreAllNoneExisting();
        }
    }
}
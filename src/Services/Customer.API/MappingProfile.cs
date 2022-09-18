using AutoMapper;
using Customer.API.Entites;

namespace Customer.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatalogCustomer, CustomerDTO>();
            CreateMap<CreateCustomerDTO, CatalogCustomer>();
            CreateMap<UpdateCustomerDTO, CatalogCustomer>();
        }
    }
}

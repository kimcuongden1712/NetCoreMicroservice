using AutoMapper;
using Customer.API.Entities;
using Infrastructure.Mapping;
using Shared.DTOs.Customer;

namespace Customer.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CatelogCustomer, CustomerDTO>();
            CreateMap<CreateCustomerDTO, CatelogCustomer>();
            CreateMap<UpdateCustomerDTO, CatelogCustomer>().IgnoreAllNonSetting();
        }
    }
}
using AutoMapper;
using Customer.API.Entities;
using Customer.API.Services.Interfaces;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Customer;
using Shared.DTOs.Product;

namespace Customer.API.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IResult> CreateCustomerAsync(CreateCustomerDTO customerDTO)
        {
            var entity = await _repository.GetCustomerByEmail(customerDTO.EmailAddress);
            if (entity != null)
            {
                return Results.BadRequest($"Customer with email {customerDTO.EmailAddress} already exists.");
            }
            var customer = _mapper.Map<CatelogCustomer>(customerDTO);
            await _repository.CreateCustomer(customer);
            await _repository.SaveChangeAsync();
            var result = _mapper.Map<CustomerDTO>(customer);
            return Results.Ok(result);
        }

        public async Task<IResult> DeleteCustomerAsync(int id)
        {
            var product = await _repository.GetCustomerAsync(id);
            if (product == null)
            {
                return Results.NotFound();
            }
            await _repository.DeleteCustomer(id);
            await _repository.SaveChangeAsync();
            return Results.NoContent();
        }

        public async Task<IResult> GetCustomersAsync() => Results.Ok(await _repository.GetCustomersAsync());

        public async Task<IResult> GetCustomersByUserNameAsync(string userName) => Results.Ok(await _repository.GetCustomersByUserNameAsync(userName));

        public async Task<IResult> UpdateCustomerAsync(UpdateCustomerDTO customerDTO)
        {
            var entity = await _repository.GetCustomerByEmail(customerDTO.EmailAddress);
            if (entity == null)
            {
                return Results.NotFound($"Customer with email {customerDTO.EmailAddress} not found.");
            }
            var customer = _mapper.Map(customerDTO, entity);
            await _repository.UpdateAsync(customer);
            await _repository.SaveChangeAsync();
            var result = _mapper.Map<CustomerDTO>(customer);
            return Results.Ok(result);
        }
    }
}
using Customer.API.Repositories.Interfaces;

namespace Customer.API.Services.Interfaces
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IResult> GetCustomerByIdAsync(long id) => Results.Ok(await _customerRepository.GetCustomer(id));

        public async Task<IResult> GetCustomersAsync() => Results.Ok(await _customerRepository.GetCustomers());

        public async Task<IResult> GetCustomerByEmailAsync(string email) => Results.Ok(await _customerRepository.GetCustomerByEmailAddress(email));
    }
}
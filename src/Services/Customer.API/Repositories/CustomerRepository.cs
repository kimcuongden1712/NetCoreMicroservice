using Contracts.Common.Interfaces;
using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Repositories.Interfaces
{
    public class CustomerRepository : RepositoryBaseAsync<Entities.Customer, long, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<Entities.Customer>> GetCustomers() => await FindAll().ToListAsync();

        public Task<Entities.Customer> GetCustomer(long id) => GetByIdAsync(id);

        public Task<Entities.Customer> GetCustomerByEmailAddress(string email) => FindByCondition(x => x.EmailAddress.Equals(email)).SingleOrDefaultAsync();

        public Task CreateCustomer(Entities.Customer customer) => CreateAsync(customer);

        public Task UpdateCustomer(Entities.Customer customer) => UpdateAsync(customer);

        public async Task DeleteCustomer(long id)
        {
            var customer = await GetCustomer(id);
            if (customer != null) DeleteAsync(customer);
        }
    }
}
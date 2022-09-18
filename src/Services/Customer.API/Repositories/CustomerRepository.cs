using Contracts.Common.Interfaces;
using Customer.API.Entites;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Product.API.Repositories.Interfaces
{
    public class CustomerRepository : RepositoryBaseAsync<CatalogCustomer, long, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<CatalogCustomer>> GetCustomers() => await FindAll().ToListAsync();

        public Task<CatalogCustomer> GetCustomer(long id) => GetByIdAsync(id);

        public Task<CatalogCustomer> GetCustomerByEmailAddress(string email) => FindByCondition(x => x.EmailAddress.Equals(email)).SingleOrDefaultAsync();

        public Task CreateCustomer(CatalogCustomer customer) => CreateAsync(customer);

        public Task UpdateCustomer(CatalogCustomer customer) => UpdateAsync(customer);

        public async Task DeleteCustomer(long id)
        {
            var customer = await GetCustomer(id);
            if (customer != null) DeleteAsync(customer);
        }
    }
}
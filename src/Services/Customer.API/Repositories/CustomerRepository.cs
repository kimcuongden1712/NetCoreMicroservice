using Contracts.Commonn.Interfaces;
using Customer.API.Entities;
using Customer.API.Persistence;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Repositories.Interfaces;

namespace Customer.API.Repositories
{
    public class CustomerRepository : RepositoryBaseAsync<CatelogCustomer, int, CustomerContext>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext dbContext, IUnitOfWork<CustomerContext> unitOfWork) : base(dbContext, unitOfWork)
        {
        }

        public async Task<IEnumerable<CatelogCustomer>> GetCustomersAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<CatelogCustomer> GetCustomerAsync(int id)
        {
            return await FindByCondition(p => p.Id.Equals(id), trackChanges: false).SingleOrDefaultAsync();
        }

        public async Task<CatelogCustomer> GetCustomerByEmail(string email)
        {
            return await FindByCondition(p => p.EmailAddress.Equals(email), trackChanges: false).SingleOrDefaultAsync();
        }

        public async Task CreateCustomer(CatelogCustomer customer) => await CreateAsync(customer);

        public async Task UpdateCustomer(CatelogCustomer customer) => await UpdateAsync(customer);

        public async Task DeleteCustomer(int id)
        {
            var customer = await GetCustomerAsync(id);
            await DeleteAsync(customer);
        }
    }
}

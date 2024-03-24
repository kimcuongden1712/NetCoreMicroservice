using Contracts.Commonn.Interfaces;
using Customer.API.Entities;
using Customer.API.Persistence;

namespace Product.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<CatelogCustomer, int, CustomerContext>
    {
        Task<IEnumerable<CatelogCustomer>> GetCustomersAsync();
        Task<CatelogCustomer> GetCustomerAsync(int id);
        Task<CatelogCustomer> GetCustomerByEmail(string emailAddress);
        Task CreateCustomer(CatelogCustomer product);
        Task UpdateCustomer(CatelogCustomer product);
        Task DeleteCustomer(int id);
    }
}

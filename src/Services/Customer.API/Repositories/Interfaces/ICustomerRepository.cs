using Contracts.Common.Interfaces;
using Customer.API.Entites;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<CatalogCustomer, long, CustomerContext>
    {
        Task<IEnumerable<CatalogCustomer>> GetCustomers();
        Task<CatalogCustomer> GetCustomer(long id);
        Task<CatalogCustomer> GetCustomerByEmailAddress(string email);
        Task CreateCustomer(CatalogCustomer customer);
        Task UpdateCustomer(CatalogCustomer customer);
        Task DeleteCustomer(long id);
    }
}

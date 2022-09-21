using Contracts.Common.Interfaces;
using Customer.API.Persistence;

namespace Customer.API.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBaseAsync<Entities.Customer, long, CustomerContext>
    {
        Task<IEnumerable<Entities.Customer>> GetCustomers();

        Task<Entities.Customer> GetCustomer(long id);

        Task<Entities.Customer> GetCustomerByEmailAddress(string email);

        Task CreateCustomer(Entities.Customer customer);

        Task UpdateCustomer(Entities.Customer customer);

        Task DeleteCustomer(long id);
    }
}
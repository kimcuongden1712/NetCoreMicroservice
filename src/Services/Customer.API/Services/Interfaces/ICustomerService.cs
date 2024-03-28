using Shared.DTOs.Customer;

namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomersByUserNameAsync(string userName);

        Task<IResult> GetCustomersAsync();

        Task<IResult> CreateCustomerAsync(CreateCustomerDTO customer);

        Task<IResult> UpdateCustomerAsync(UpdateCustomerDTO customer);

        Task<IResult> DeleteCustomerAsync(int customerId);
    }
}
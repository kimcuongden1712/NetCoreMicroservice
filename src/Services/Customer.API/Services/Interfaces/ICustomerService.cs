namespace Customer.API.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IResult> GetCustomersAsync();

        Task<IResult> GetCustomerByIdAsync(long id);

        Task<IResult> GetCustomerByEmailAsync(string email);
    }
}
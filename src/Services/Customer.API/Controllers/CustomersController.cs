using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;

namespace Customer.API.Controllers
{
    public static class CustomersController
    {
        public static void MapCustomersAPI(this WebApplication app)
        {
            app.MapGet("/api/customers/",
                async (ICustomerService customerService) =>
                {
                    var result = await customerService.GetCustomersAsync();
                    return result != null ? result : Results.NotFound();
                });
        }

        public static void CustomerByEmailAPI(this WebApplication app)
        {
            app.MapGet("/api/customers/{email}", async (string email, ICustomerService customerService) =>
            {
                var customer = await customerService.GetCustomerByEmailAsync(email);
                return customer != null ? Results.Ok(customer) : Results.NotFound();
            });
        }

        public static void MapCreateCustomersAPI(this WebApplication app)
        {
            //Call Repository
            app.MapPost("/api/customers/", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
            {
                await customerRepository.CreateAsync(customer);
                await customerRepository.SaveChangesAsync();
            });
        }

        public static void MapDeleteCustomersAPI(this WebApplication app)
        {
            //Call Repository
            app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
            {
                var customer = await customerRepository.GetCustomer(id);
                if (customer == null) return Results.NotFound();
                await customerRepository.DeleteAsync(customer);
                await customerRepository.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}
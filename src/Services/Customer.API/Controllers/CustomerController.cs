using Customer.API.Services.Interfaces;
using Shared.DTOs.Customer;

namespace Customer.API.Controllers
{
    public static class CustomerController
    {
        public static void MapCustomerController(this WebApplication app)
        {
            app.MapGet("/api/customers", async (ICustomerService customerService) => await customerService.GetCustomersAsync());
            app.MapGet("/api/customers/{userName}", async (ICustomerService customerService, string userName) => await customerService.GetCustomersByUserNameAsync(userName));
            app.MapPost("/api/customers", async (ICustomerService customerService, CreateCustomerDTO customer) =>
            {
                await customerService.CreateCustomerAsync(customer);
                return Results.Created($"/api/customers/{customer.UserName}", customer);
            });
            app.MapPut("/api/customers", async (ICustomerService customerService, UpdateCustomerDTO customer) =>
            {
                await customerService.UpdateCustomerAsync(customer);
                return Results.Ok();
            });
            app.MapDelete("/api/customers/{id}", async (ICustomerService customerService, int id) =>
            {
                await customerService.DeleteCustomerAsync(id);
                return Results.NoContent();
            });
        }
    }
}
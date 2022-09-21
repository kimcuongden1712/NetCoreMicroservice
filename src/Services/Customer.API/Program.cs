using Common.Logging;
using Customer.API.Extentions;
using Customer.API.Persistence;
using Customer.API.Repositories.Interfaces;
using Customer.API.Services.Interfaces;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Customer API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();
    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);

    var app = builder.Build();

    #region Mapper URL minimal API

    app.MapGet("/", () => "Wellcome to Customer API");
    //app.MapPost("/", () => "Wellcome to Customer API");
    //app.MapPut("/", () => "Wellcome to Customer API");
    //app.MapDelete("/", () => "Wellcome to Customer API");

    //Call service
    app.MapGet("/api/customers/", async (ICustomerService customerService) => await customerService.GetCustomersAsync());
    app.MapGet("/api/customers/{email}", async (string email, ICustomerService customerService) =>
    {
        var customer = await customerService.GetCustomerByEmailAsync(email);
        return customer != null ? Results.Ok(customer) : Results.NotFound();
    });

    //Call Repository
    app.MapPost("/api/customers/", async (Customer.API.Entities.Customer customer, ICustomerRepository customerRepository) =>
    {
        await customerRepository.CreateAsync(customer);
        await customerRepository.SaveChangesAsync();
    });

    //Call Repository
    app.MapDelete("/api/customers/{id}", async (int id, ICustomerRepository customerRepository) =>
    {
        var customer = await customerRepository.GetCustomer(id);
        if(customer == null) return Results.NotFound();
        await customerRepository.DeleteAsync(customer);
        await customerRepository.SaveChangesAsync();
        return Results.NoContent();
    });

    #endregion Mapper URL minimal API

    app.UseInfrastructure();
    app.SeedCustomerData().Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Ordering API complete");
    Log.CloseAndFlush();
}
using Common.Logging;
using Contracts.Commonn.Interfaces;
using Customer.API;
using Customer.API.Extensions;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Customer.API.Services;
using Customer.API.Services.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Repositories.Interfaces;
using Serilog;
using Shared.DTOs.Customer;

Log.Information("Starting API");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(Serilogger.Configure);

    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    var conns = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(options => options.UseNpgsql(conns));

    builder.Services.AddScoped<ICustomerRepository, CustomerRepository>()
        .AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>))
        .AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
        .AddScoped<ICustomerService, CustomerService>();
    //Add automapper
    builder.Services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));

    var app = builder.Build();

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

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection(); uncomment this line to enable https redirection

    app.MigrationDatabase<CustomerContext>((context, _) =>
    {
        CustomerContextSeed.SeedAsync(context, Log.Logger).Wait();
    }).Run();

    app.SeedCustomerData().Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.Information("Shutting down API complete");
    Log.CloseAndFlush();
}
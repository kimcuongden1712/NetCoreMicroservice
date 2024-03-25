using Common.Logging;
using Customer.API.Extensions;
using Customer.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
    builder.Services.AddDbContext<CustomerContext>(options =>
    {
        options.UseNpgsql(connectionString).EnableSensitiveDataLogging();
    });
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //app.UseHttpsRedirection(); uncomment this line to enable https redirection

    //app.MigrationDatabase<CustomerContext>((context, _) =>
    //{
    //    CustomerContextSeed.SeedAsync(context, Log.Logger).Wait();
    //}).Run();
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
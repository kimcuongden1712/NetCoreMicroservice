using Common.Logging;
using Customer.API.Controllers;
using Customer.API.Extentions;
using Customer.API.Persistence;
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

    app.Index();

    #region Mapper Customer URL minimal API

    ///api/customers/
    app.MapCustomersAPI();
    app.CustomerByEmailAPI();
    app.MapCreateCustomersAPI();
    app.MapDeleteCustomersAPI();

    #endregion Mapper Customer URL minimal API

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
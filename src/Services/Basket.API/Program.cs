using Basket.API.Extentions;
using Common.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Information("Start Basket API up");

try
{
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();
    // Add services to the container.
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    app.UseInfrastructure();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("StopTheHostException", StringComparison.Ordinal)) throw;

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down Basket API complete");
    Log.CloseAndFlush();
}
using Common.Logging;
using Product.API.Extensions;
using Serilog;

Log.Information("Starting API");

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog(Serilogger.Configure);
    builder.Host.AddAppConfigurations();
    builder.Services.AddInfrastructure(builder.Configuration);
    var app = builder.Build();
    app.UseInfrastructure();
    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if(type.Equals("StopTheHostException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, $"Unhandled exception: {ex.Message}");
}
finally
{
    Log.Information("Shutting down API complete");
    Log.CloseAndFlush();
}
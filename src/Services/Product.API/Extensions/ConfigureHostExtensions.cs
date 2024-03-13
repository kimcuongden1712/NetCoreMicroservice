namespace Product.API.Extensions
{
    public static class ConfigureHostExtensions
    {
        public static void AddAppConfigurations(this ConfigureHostBuilder host)
        {
            host.ConfigureAppConfiguration((hostingContext, config) =>
                       {
                           var env = hostingContext.HostingEnvironment;
                           config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                               .AddEnvironmentVariables();
                       });
        }
    }
}
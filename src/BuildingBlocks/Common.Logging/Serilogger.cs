using Microsoft.Extensions.Hosting;
using Serilog;

namespace Common.Logging
{
    public static class Serilogger
    {
        public static Action<HostBuilderContext, LoggerConfiguration> Configure =>
        (context, configuration) =>
        {
            var applicationName = context.HostingEnvironment.ApplicationName?.ToLower().Replace(".", "-");
            var environmentName = context.HostingEnvironment.EnvironmentName ?? "Development";

            configuration
                .WriteTo.Debug()
                .WriteTo.Console(outputTemplate: "[{TimeStamp:HH:mm:ss} {Level} {SourceContent}-{NewLine}-{Message:lj}-{NewLine}-{Exception}-{NewLine}]")
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("ApplicationName", applicationName)
                .Enrich.WithProperty("EnvironmentName", environmentName)
                .Enrich.WithProperty("SourceContent", "SourceContext")
                .ReadFrom.Configuration(context.Configuration);
        };

    }
}
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

        public static void LogException(Exception ex)
        {
            var exceptionToMessageMap = new Dictionary<string, string>
            {
                { "FileNotFoundException", "The file was not found" },
                { "HttpRequestException", "The request failed" },
                { "SqlException", "The SQL Server failed" },
                { "IOException", "An IO exception occurred" },
                { "InvalidOperationException", "An invalid operation occurred" },
                { "NotSupportedException", "The operation is not supported" },
                { "ObjectDisposedException", "The object was disposed" },
                { "UnauthorizedAccessException", "The operation was unauthorized" },
                { "ArgumentException", "An argument exception occurred" },
                { "FormatException", "The format was invalid" },
                { "NullReferenceException", "The reference was null" },
                { "IndexOutOfRangeException", "The index was out of range" },
                { "TimeoutException", "The operation timed out" },
                { "KeyNotFoundException", "The key was not found" },
                { "AggregateException", "An aggregate exception occurred" },
                { "TaskCanceledException", "The task was canceled" },
                { "OperationCanceledException", "The operation was canceled" },
                { "OverflowException", "The operation overflowed" },
                { "DivideByZeroException", "The operation divided by zero" },
                { "InvalidCastException", "The cast was invalid" }
            };

            string type = ex.GetType().Name;

            if (exceptionToMessageMap.TryGetValue(type, out var message))
            {
                Log.Error(message);
            }
            else
            {
                Log.Error("An unknown exception occurred");
            }
        }
    }
}
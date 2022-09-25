using Infrastructure.Configurations;

namespace Ordering.API.Extentions
{
    public static class ServiceExtensions
    {
        internal static IServiceCollection AddConfigurationSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            var emailSettings = configuration.GetSection(nameof(SMTPEmailSetting))
                .Get<SMTPEmailSetting>();
            services.AddSingleton(emailSettings);

            return services;
        }
    }
}
using Contracts.Commonn.Interfaces;
using Customer.API.Persistence;
using Customer.API.Repositories;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Product.API.Repositories.Interfaces;

namespace Customer.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.ConfigureProductDbContext(configuration);
            services.AddInfrastructureSerivices();
            services.AddAutoMapper(cfg => cfg.AddProfile(new MappingProfile()));
            return services;
        }

        private static IServiceCollection ConfigureProductDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnectionString");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            services.AddDbContext<CustomerContext>(options =>
            {
                options.UseNpgsql(builder.ConnectionString, e =>
                {
                    e.MigrationsAssembly("Customer.API");
                });
            });

            return services;
        }

        private static IServiceCollection AddInfrastructureSerivices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryBaseAsync<,,>), typeof(RepositoryBaseAsync<,,>));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
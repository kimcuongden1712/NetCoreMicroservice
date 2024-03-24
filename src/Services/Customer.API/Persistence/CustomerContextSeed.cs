﻿using Customer.API.Entities;
using Microsoft.EntityFrameworkCore;
using Ilogger = Serilog.ILogger;

namespace Customer.API.Persistence
{
    public static class CustomerContextSeed
    {
        public static async Task SeedAsync(CustomerContext customerContext, Ilogger logger)
        {
            if (!customerContext.Customers.Any())
            {
                customerContext.AddRange(GetCustomer());
                
                await customerContext.SaveChangesAsync();

                logger.Information("Seed database associated with context {DbContextName}", typeof(CustomerContext).Name);
            }
        }

        private static IEnumerable<CatelogCustomer> GetCustomer()
        {
            return new List<CatelogCustomer>
            {
                new CatelogCustomer
                {
                    Username = "johndoe",
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "abc@gmail.com"
                },
                new CatelogCustomer
                {
                    Username = "janedoe",
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailAddress = "bcd@gmail.com"
                },
                new CatelogCustomer
                {
                    Username = "admin",
                    FirstName = "Admin",
                    LastName = "Supper",
                    EmailAddress = "admin@gmail.com"
                },
                new CatelogCustomer
                {
                    Username = "user",
                    FirstName = "User",
                    LastName = "Normal",
                    EmailAddress = "user@gmail.com"
                }
            };
        }
        
        public static IHost SeedCustomerData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<Ilogger>();
                var context = services.GetRequiredService<CustomerContext>();

                try
                {
                    context.Database.MigrateAsync().GetAwaiter().GetResult();

                    CreateCustomer(context, new CatelogCustomer
                    {
                        Username = "Odegat",
                        FirstName = "Rizenn",
                        LastName = "Doe",
                        EmailAddress = "Odegat@gmail.com"
                    });

                    CreateCustomer(context, new CatelogCustomer
                    {
                        Username = "kai",
                        FirstName = "Havert",
                        LastName = "Kai",
                        EmailAddress = "kaihavert@gmail.com"
                    });
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "An error occurred seeding the database.");
                }
            }

            return host;
        }

        private static async Task CreateCustomer(CustomerContext context, CatelogCustomer c)
        {
            var customer = await context.Customers.SingleOrDefaultAsync
                (x=>x.Username.Equals(c.Username) || x.EmailAddress.Equals(c.EmailAddress));
            if (customer == null)
            {
                var newCustomer = new CatelogCustomer
                {
                    Username = c.Username,
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    EmailAddress = c.EmailAddress
                };
                await context.Customers.AddAsync(newCustomer);
                await context.SaveChangesAsync();
            }
        }
    }
}

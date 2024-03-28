using Contracts.Domain.Interfaces;
using Customer.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Customer.API.Persistence
{
    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
        {
        }

        public DbSet<CatelogCustomer> Customers { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CatelogCustomer>().HasIndex(x => x.UserName).IsUnique();
            modelBuilder.Entity<CatelogCustomer>().HasIndex(x => x.EmailAddress).IsUnique();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifiedEntries = ChangeTracker
                .Entries()
                .Where(x => (x.State == EntityState.Added ||
                x.State == EntityState.Modified ||
                x.State == EntityState.Deleted));
            foreach (var entry in modifiedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IDateTracking addedEntity)
                        {
                            addedEntity.CreatedDate = DateTime.UtcNow;
                            //addedEntity.CreateUser = currentUser.UserName;
                            entry.State = EntityState.Added;
                        }
                        break;

                    case EntityState.Modified:
                        Entry(entry.Entity).Property("Id").IsModified = false;
                        if (entry.Entity is IDateTracking modifiedEntity)
                        {
                            modifiedEntity.LastModifiedDate = DateTime.UtcNow;
                            entry.State = EntityState.Modified;
                        }
                        break;

                    case EntityState.Deleted:
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
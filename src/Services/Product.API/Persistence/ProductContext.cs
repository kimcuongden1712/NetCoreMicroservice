using Contracts.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;

namespace Product.API.Persistence
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }

        public DbSet<CatelogProduct> Products { get; set; } = default!;

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
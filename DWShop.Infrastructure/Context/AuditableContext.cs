using DWShop.Domain.Contracts;
using DWShop.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class AuditableContext : IdentityDbContext
    {
        public AuditableContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Catalog> Catalog { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries<IAuditableEntity>().ToList())
            {
                switch (item.State)
                {
                    case EntityState.Modified:
                        item.Entity.LastModifyedOn = DateTime.UtcNow;
                        item.Entity.LastModifiedBy = "User";
                        break;
                    case EntityState.Added:
                        item.Entity.CreatedBy = "User";
                        item.Entity.CreatedOn = DateTime.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}

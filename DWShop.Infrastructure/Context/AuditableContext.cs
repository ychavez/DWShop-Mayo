using DWShop.Domain.Contracts;
using DWShop.Domain.Entities;
using DWShop.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DWShop.Infrastructure.Context
{
    public class AuditableContext : IdentityDbContext
    {
        public AuditableContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Catalog> Catalog { get; set; }
        public DbSet<Audit> Audit { get; set; }
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


        private List<AuditEntry> onBeforeSaveChanges(string userId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();

            foreach (var entry in ChangeTracker.Entries())
            {

                if (entry.Entity is Audit
                    || entry.State == EntityState.Detached
                    || entry.State == EntityState.Unchanged
                    )
                {
                    continue;
                }

                var auditEntry = new AuditEntry(entry)
                {
                    TableName = entry.Entity.GetType().Name,
                    UserId = userId
                };

                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    var propertyName = property.Metadata.Name;

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue!;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            break;
                        case EntityState.Modified:
                            if (property.IsModified && property.OriginalValue?.Equals(property.CurrentValue) == false)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                            }
                            break;
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                            break;
  
                    }
                }
            }

            foreach (var auditEntry in auditEntries.Where(x=> !x.HasTemporaryProperties))
            {
                Audit.Add(auditEntry.ToAudit());
            }

            return auditEntries.Where(x => !x.HasTemporaryProperties).ToList();
           
        }
    }
}

using DWShop.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace DWShop.Domain.Models
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entity)
        {
            Entity = entity;
        }

        public EntityEntry Entity { get; set; }

        public string UserId { get; set; }
        public string TableName { get; set; }

        public Dictionary<string, object> KeyValues { get; set; } = new();
        public Dictionary<string, object> OldValues { get; set; } = new();
        public Dictionary<string, object> NewValues { get; set; } = new();

        public List<PropertyEntry> TemporaryProperties { get; set; } = new();

        public AuditType AuditType { get; set; }
        public List<string> ChangedColumns { get; set; }

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public Audit ToAudit()
        {

            var audit = new Audit
            {
                UserId = UserId,
                TableName = TableName,
                Type = AuditType.ToString(),
                DateTime = DateTime.UtcNow,
                PrimaryKey = JsonSerializer.Serialize(KeyValues),
                OldValues = OldValues.Any() ? JsonSerializer.Serialize(OldValues) : null,
                NewValues = NewValues.Any() ? JsonSerializer.Serialize(NewValues) : null,
                AffectedColumns = ChangedColumns.Any() ? JsonSerializer.Serialize(ChangedColumns) : null
            };
            return audit;
        }
    }

    public enum AuditType
    {
        none = 0,
        Create = 1,
        Update = 2,
        Delete = 3
    }
}

﻿using DWShop.Domain.Contracts;

namespace DWShop.Domain.Entities
{
    public class Catalog : AuditableEntity<int>
    {
        public string Name { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public decimal Price { get; set; }


        public string? PhotoURL { get; set; }

    }
}

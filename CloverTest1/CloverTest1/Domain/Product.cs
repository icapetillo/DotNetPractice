using System;
using System.Collections.Generic;

namespace CloverTest1.Domain
{
    public class Product
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }

        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; } // decimal in main units (e.g., dollars)

        public string? Category { get; set; }

        // Example for modifiers (simple representation)
        public List<ProductModifier> Modifiers { get; set; } = new();
    }

    public class ProductModifier
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; } = null!;
        public decimal PriceDelta { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace CloverTest1.Domain
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<OrderLine> Lines { get; set; } = new();
    }

    public class OrderLine
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}

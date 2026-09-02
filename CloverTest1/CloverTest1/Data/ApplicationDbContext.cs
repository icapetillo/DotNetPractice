using CloverTest1.Domain;
using Microsoft.EntityFrameworkCore;

namespace CloverTest1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ProductModifier> ProductModifiers { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderLine> OrderLines { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Tenant>(b =>
            {
                b.HasKey(t => t.Id);
                b.Property(t => t.Name).IsRequired();
            });

            modelBuilder.Entity<Product>(b =>
            {
                b.HasKey(p => p.Id);
                b.Property(p => p.Name).IsRequired();
                b.Property(p => p.Price).HasColumnType("decimal(18,2)");
                b.HasMany(p => p.Modifiers).WithOne().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ProductModifier>(b =>
            {
                b.HasKey(m => m.Id);
                b.Property(m => m.Name).IsRequired();
                b.Property(m => m.PriceDelta).HasColumnType("decimal(18,2)");
            });

            modelBuilder.Entity<Order>(b =>
            {
                b.HasKey(o => o.Id);
                b.Property(o => o.CreatedAt).IsRequired();
                b.HasMany(o => o.Lines).WithOne().HasForeignKey(l => l.OrderId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderLine>(b =>
            {
                b.HasKey(l => l.Id);
                b.Property(l => l.UnitPrice).HasColumnType("decimal(18,2)");
            });

            // Global query filter example for multi-tenancy can be applied at runtime via a service.
        }
    }
}

using System;

namespace CloverTest1.Domain
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? ConnectionString { get; set; }

        // Additional metadata for white-label
        public string? BrandDisplayName { get; set; }
        public string? ApiKey { get; set; }

        // Clover OAuth fields (per-tenant credentials/tokens)
        public string? CloverClientId { get; set; }
        public string? CloverClientSecret { get; set; }

        public string? CloverAccessToken { get; set; }
        public string? CloverRefreshToken { get; set; }
        public DateTime? CloverTokenExpiresAt { get; set; }
    }
}

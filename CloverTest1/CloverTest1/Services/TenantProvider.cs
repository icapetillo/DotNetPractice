using System;
using Microsoft.AspNetCore.Http;
using CloverTest1.Data;
using System.Linq;
using CloverTest1.Domain;
using System.Threading.Tasks;

namespace CloverTest1.Services
{
    // Simple tenant provider that reads a header "X-Tenant-Id" (GUID)
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationDbContext _db;

        public TenantProvider(IHttpContextAccessor httpContextAccessor, ApplicationDbContext db)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }

        public Tenant GetCurrentTenant()
        {
            // Blocking call for convenience; prefers async in new code
            return GetCurrentTenantAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        public async Task<Tenant> GetCurrentTenantAsync()
        {
            var ctx = _httpContextAccessor.HttpContext;
            if (ctx == null)
                throw new InvalidOperationException("No HttpContext available");

            if (!ctx.Request.Headers.TryGetValue("X-Tenant-Id", out var values))
                throw new InvalidOperationException("Tenant header missing");

            if (!Guid.TryParse(values.First(), out var tenantId))
                throw new InvalidOperationException("Invalid tenant id");

            var tenant = await _db.Tenants.FindAsync(tenantId);
            if (tenant == null)
                throw new InvalidOperationException("Tenant not found");

            return tenant;
        }
    }
}

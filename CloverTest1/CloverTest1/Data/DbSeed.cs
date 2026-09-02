using System;
using System.Linq;
using CloverTest1.Domain;

namespace CloverTest1.Data
{
    public static class DbSeed
    {
        public static void EnsureSeed(ApplicationDbContext db)
        {
            if (!db.Tenants.Any())
            {
                var t1 = new Tenant
                {
                    Id = Guid.NewGuid(),
                    Name = "demo-no-alcohol",
                    BrandDisplayName = "Demo No Alcohol",
                    CloverClientId = "demo-client-id-1",
                    CloverClientSecret = "demo-client-secret-1",
                    CloverAccessToken = null,
                    CloverRefreshToken = null,
                    CloverTokenExpiresAt = null
                };

                var t2 = new Tenant
                {
                    Id = Guid.NewGuid(),
                    Name = "demo-all",
                    BrandDisplayName = "Demo All",
                    CloverClientId = "demo-client-id-2",
                    CloverClientSecret = "demo-client-secret-2",
                    CloverAccessToken = null,
                    CloverRefreshToken = null,
                    CloverTokenExpiresAt = null
                };

                db.Tenants.AddRange(t1, t2);
                db.SaveChanges();
            }
        }
    }
}

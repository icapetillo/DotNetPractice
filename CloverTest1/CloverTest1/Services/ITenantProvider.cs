using System;
using System.Threading.Tasks;
using CloverTest1.Domain;

namespace CloverTest1.Services
{
    public interface ITenantProvider
    {
        Tenant GetCurrentTenant();
        Task<Tenant> GetCurrentTenantAsync();
    }
}

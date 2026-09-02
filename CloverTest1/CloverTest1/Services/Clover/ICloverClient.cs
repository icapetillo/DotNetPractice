using System.Threading.Tasks;
using CloverTest1.Services.Clover.Models;
using System.Collections.Generic;

namespace CloverTest1.Services.Clover
{
    public interface ICloverClient
    {
        Task<IEnumerable<CloverItem>> GetItemsAsync();

        // Force token refresh for current tenant
        Task ForceRefreshTokenAsync();
    }
}

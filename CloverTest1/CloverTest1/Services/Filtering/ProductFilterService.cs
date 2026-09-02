using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloverTest1.Domain;
using CloverTest1.Services;

namespace CloverTest1.Services.Filtering
{
    public class ProductFilterService : IProductFilterService
    {
        private readonly ITenantProvider _tenantProvider;

        public ProductFilterService(ITenantProvider tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        public Task<IEnumerable<Product>> ApplyFiltersAsync(IEnumerable<Product> items)
        {
            var tenant = _tenantProvider.GetCurrentTenant();

            var result = items;

            if (!string.IsNullOrEmpty(tenant.Name) && tenant.Name.ToLower().Contains("no-alcohol"))
            {
                result = result.Where(p => !(p.Category?.ToLower() == "alcohol"));
            }

            return Task.FromResult(result);
        }
    }
}

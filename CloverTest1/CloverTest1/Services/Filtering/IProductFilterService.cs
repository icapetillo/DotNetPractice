using System.Collections.Generic;
using System.Threading.Tasks;
using CloverTest1.Domain;

namespace CloverTest1.Services.Filtering
{
    public interface IProductFilterService
    {
        Task<IEnumerable<Product>> ApplyFiltersAsync(IEnumerable<Product> items);
    }
}

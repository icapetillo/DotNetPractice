using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CloverTest1.Services.Clover;
using System.Linq;
using CloverTest1.Services.Filtering;
using CloverTest1.Domain;
using System.Collections.Generic;

namespace CloverTest1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CloverController : ControllerBase
    {
        private readonly ICloverClient _clover;
        private readonly IProductFilterService _filter;

        public CloverController(ICloverClient clover, IProductFilterService filter)
        {
            _clover = clover;
            _filter = filter;
        }

        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _clover.GetItemsAsync();

            // Map to domain products
            var products = items.Select(i => new Product
            {
                Id = System.Guid.NewGuid(),
                Name = i.Name ?? "",
                Description = i.Description,
                Price = i.Price ?? 0m,
                Category = i.Category
            }).ToList();

            var filtered = await _filter.ApplyFiltersAsync(products);

            return Ok(filtered);
        }

        [HttpPost("force-refresh-token")]
        public async Task<IActionResult> ForceRefreshToken()
        {
            await _clover.ForceRefreshTokenAsync();
            return NoContent();
        }
    }
}

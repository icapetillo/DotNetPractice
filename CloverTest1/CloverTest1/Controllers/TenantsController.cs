using Microsoft.AspNetCore.Mvc;
using System.Linq;
using CloverTest1.Data;

namespace CloverTest1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TenantsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public TenantsController(ApplicationDbContext db) => _db = db;

        // Development helper: list tenants to verify IDs
        [HttpGet]
        public IActionResult GetAll()
        {
            var tenants = _db.Tenants.Select(t => new { t.Id, t.Name, t.BrandDisplayName }).ToList();
            return Ok(tenants);
        }
    }
}

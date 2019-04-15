using System.Linq;
using System.Threading.Tasks;
using company.inventory.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace company.inventory.graph.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly InventoryContext context;

        public ProductController(InventoryContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var results = await context.Products.Include(s => s.Stock).Include(ps => ps.Services).ToListAsync();
            var response = results.Select(r => new
            {
                name = r.Name,
                description = r.Description,
                available = r.Stock.Available,
                reserved = r.Stock.Reserved,
                services = r.Services.Select(s => new
                {
                    name = s.Name,
                    required = s.Required
                })
            });
            return Ok(response);
        }
    }
}
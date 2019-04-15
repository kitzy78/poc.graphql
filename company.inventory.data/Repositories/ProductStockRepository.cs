using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using company.inventory.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace company.inventory.data.Repositories
{
    public class ProductStockRepository : IRepository<ProductStock>
    {
        private readonly InventoryContext context;

        public ProductStockRepository(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<ProductStock> Get(int id)
        {
            return await context.Stock.FirstOrDefaultAsync(s => s.ProductStockId == id);
        }

        public async Task<ICollection<ProductStock>> Get(ProductStock criteria)
        {
            var query = context.Stock.AsQueryable();
            if (criteria.Available > 0) query = query.Where(q => q.Available == criteria.Available);
            if (criteria.Reserved > 0) query = query.Where(q => q.Reserved == criteria.Reserved);
            return await query.ToListAsync();
        }

        public async Task<ProductStock> Set(ProductStock value)
        {
            return value;
        }

        public async Task<ProductStock> Create(ProductStock value)
        {
            return value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using company.inventory.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace company.inventory.data.Repositories
{
    public class ProductServiceRepository : IRepository<ProductService>, ILookupById<ProductService>
    {
        private readonly InventoryContext context;

        public ProductServiceRepository(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<ProductService> Get(int id)
        {
            return await context.ProductServices.FirstOrDefaultAsync(p => p.ProductServiceId == id);
        }

        public async Task<ICollection<ProductService>> Get(ProductService criteria)
        {
            var query = context.ProductServices.AsQueryable();
            if (!string.IsNullOrEmpty(criteria.Name)) query = query.Where(q => q.Name.Equals(criteria.Name, StringComparison.InvariantCultureIgnoreCase));
            return await query.ToListAsync();
        }

        public Task<ProductService> Set(ProductService value)
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductService> Create(ProductService value)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ILookup<int, ProductService>> GetById(IEnumerable<int> ids)
        {
            var results = await context.ProductServices.Where(p => ids.Contains(p.ProductId)).ToListAsync();
            return results.ToLookup(l => l.ProductId);
        }
    }
}
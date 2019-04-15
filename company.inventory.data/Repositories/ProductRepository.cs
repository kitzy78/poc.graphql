using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using company.inventory.data.Entities;
using Microsoft.EntityFrameworkCore;

namespace company.inventory.data.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly InventoryContext context;

        public ProductRepository(InventoryContext context)
        {
            this.context = context;
        }

        public async Task<Product> Get(int id)
        {
            var result = await context.Products
                .Include(p => p.Stock)
                .Include(p => p.Services)
                .FirstOrDefaultAsync(p => p.ProductId == id);
            return result;
        }

        public async Task<ICollection<Product>> Get(Product criteria)
        {
            //Fetch an unfiltered query
            var query = context.Products.AsQueryable();
            //Build the query filter or predicate by checking which criteria have been passed in
            var predicate = PredicateBuilder.True<Product>();
            if (!string.IsNullOrEmpty(criteria?.Name)) predicate = predicate.And(p => string.Compare(p.Name, criteria.Name, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (!string.IsNullOrEmpty(criteria?.Description)) predicate = predicate.And(p => string.Compare(p.Description, criteria.Description, StringComparison.InvariantCultureIgnoreCase) == 0);
            //Apply the predicate to the query and return realized query
            var results = await query.Where(predicate).Include(p => p.Stock).Include(p2 => p2.Services).ToListAsync();
            return results;
        }

        public async Task<Product> Set(Product value)
        {
            var product = await context.Products.FirstOrDefaultAsync(p => p.ProductId == value.ProductId);
            if (!string.IsNullOrEmpty(value.Name)) product.Name = value.Name;
            if (!string.IsNullOrEmpty(value.Description)) product.Description = value.Description;
            await context.SaveChangesAsync();
            return await context.Products.FirstOrDefaultAsync(p => p.ProductId == value.ProductId);
        }

        public async Task<Product> Create(Product value)
        {
            var result = await context.Products.AddAsync(value);
            await context.SaveChangesAsync();
            return result.Entity;
        }
    }
}
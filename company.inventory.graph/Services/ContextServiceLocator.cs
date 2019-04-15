using company.inventory.data.Entities;
using company.inventory.data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace company.inventory.graph.Services
{
    public class ContextServiceLocator
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ContextServiceLocator(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public IRepository<Product> ProductRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IRepository<Product>>();
        public IRepository<ProductStock> ProductStockRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IRepository<ProductStock>>();
        public IRepository<ProductService> ProductServiceRepository => httpContextAccessor.HttpContext.RequestServices.GetRequiredService<IRepository<ProductService>>();
    }
}

using System;
using company.inventory.data.Entities;

namespace company.inventory.data.Extensions
{
    public static class InventorySeed
    {
        public static void Seed(this InventoryContext context)
        {
            const int RowCount = 100;
            var randomizer = new Random();

            for (var p = 1; p <= RowCount; p++)
            {
                var product = new Product
                {
                    ProductId = p,
                    Name = $"Product{p}",
                    Description = $"Product{p} Description",
                };
                context.Products.Add(product);

                var stock = new ProductStock
                {
                    ProductId = product.ProductId,
                    ProductStockId = product.ProductId * RowCount,
                    Available = randomizer.Next(1, 10),
                    Reserved = randomizer.Next(1, 10)
                };
                context.Stock.Add(stock);

                for (var i = 1; i <= 3; i++)
                {
                    var service = new ProductService
                    {
                        ProductId = product.ProductId,
                        ProductServiceId = product.ProductId * RowCount + i,
                        Name = $"Service{i}",
                        Required = randomizer.Next(0, 1) == 1
                    };
                    context.ProductServices.Add(service);
                }
            }

            context.SaveChanges();
        }
    }
}
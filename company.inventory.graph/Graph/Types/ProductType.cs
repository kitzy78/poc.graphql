using company.inventory.data.Entities;
using company.inventory.data.Repositories;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace company.inventory.graph.Graph.Types
{
    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType(IDataLoaderContextAccessor dataLoaderAccessor,
            ILookupById<ProductService> productServiceLookup)
        {
            Name = "product";

            Field(f => f.ProductId, false, typeof(IdGraphType)).Description("Product key");
            Field(f => f.Name, false, typeof(StringGraphType)).Description("Name of the product");
            Field(f => f.Description, true, typeof(StringGraphType)).Description("Description of the product");
            Field(f => f.Stock, true, typeof(ProductStockType));
            Field(f => f.Services, true, typeof(ListGraphType<ProductServiceType>));

            FieldAsync<ListGraphType<ProductServiceType>>("services2",
                resolve: async context =>
                {
                    var loader =
                        dataLoaderAccessor.Context.GetOrAddCollectionBatchLoader<int, ProductService>(
                            "GetServicesByProductId", productServiceLookup.GetById);
                    return await loader.LoadAsync(context.Source.ProductId);
                });
        }
    }
}

using company.inventory.data.Entities;
using GraphQL.Types;

namespace company.inventory.graph.Graph.Types
{
    public class ProductStockType : ObjectGraphType<ProductStock>
    {
        public ProductStockType()
        {
            Name = "productStock";

            Field(f => f.ProductStockId, false, typeof(IdGraphType)).Description("Product stock key");
            Field(f => f.Available, false, typeof(IntGraphType)).Description("Product stock available");
            Field(f => f.Reserved, false, typeof(IntGraphType)).Description("Product stock reserved");
        }
    }
}

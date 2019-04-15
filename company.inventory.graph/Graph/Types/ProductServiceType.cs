using company.inventory.data.Entities;
using GraphQL.Types;

namespace company.inventory.graph.Graph.Types
{
    public class ProductServiceType : ObjectGraphType<ProductService>
    {
        public ProductServiceType()
        {
            Name = "productService";

            Field(f => f.ProductServiceId, false, typeof(IdGraphType)).Description("Service key");
            Field(f => f.Name, false, typeof(StringGraphType)).Description("Service name");
            Field(f => f.Required, false, typeof(BooleanGraphType)).Description("Service required flag");
        }
    }
}

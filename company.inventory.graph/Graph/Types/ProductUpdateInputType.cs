using company.inventory.data.Entities;
using GraphQL.Types;

namespace company.inventory.graph.Graph.Types
{
    public class ProductUpdateInputType : InputObjectGraphType<Product>
    {
        public ProductUpdateInputType()
        {
            Name = "productUpdate";

            Field(f => f.ProductId, false, typeof(IdGraphType));
            Field(f => f.Name, true, typeof(StringGraphType));
            Field(f => f.Description, true, typeof(StringGraphType));
        }
    }
}

using company.inventory.data.Entities;
using GraphQL.Types;

namespace company.inventory.graph.Graph.Types
{
    public class ProductSearchInputType : InputObjectGraphType<Product>
    {
        public ProductSearchInputType()
        {
            Name = "productSearch";

            Field(f => f.Name, true).Description("Product name to search");
            Field(f => f.Description, true).Description("Product description to search");
        }
    }
}

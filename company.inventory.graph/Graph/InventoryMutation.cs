using company.inventory.data.Entities;
using company.inventory.graph.Graph.Types;
using company.inventory.graph.Services;
using GraphQL.Types;

namespace company.inventory.graph.Graph
{
    public class InventoryMutation : ObjectGraphType
    {
        public InventoryMutation(ContextServiceLocator contextServiceLocator)
        {
            Name = "inventoryMutation";

            FieldAsync<ProductType>("updateProduct",
                arguments: new QueryArguments(new QueryArgument<ProductUpdateInputType> {Name = "product"}),
                resolve: async context =>
                {
                    var product = context.GetArgument<Product>("product");
                    var result = await contextServiceLocator.ProductRepository.Set(product);
                    return result;
                });
        }
    }
}

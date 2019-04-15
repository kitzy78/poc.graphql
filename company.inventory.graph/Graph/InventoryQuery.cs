using company.inventory.data.Entities;
using company.inventory.graph.Graph.Types;
using company.inventory.graph.Services;
using GraphQL;
using GraphQL.Types;

namespace company.inventory.graph.Graph
{
    public class InventoryQuery : ObjectGraphType
    {
        public InventoryQuery(ContextServiceLocator contextServiceLocator)
        {
            Name = "inventory";

            FieldAsync<ProductType>("product",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var result = await contextServiceLocator.ProductRepository.Get(id);
                    return result;
                });

            FieldAsync<ListGraphType<ProductType>>("products",
                arguments: new QueryArguments(new QueryArgument<ProductSearchInputType> {Name = "search"}),
                resolve: async context =>
                {
                    var search = context.GetArgument<Product>("search");
                    var results = await contextServiceLocator.ProductRepository.Get(search);
                    return results;
                });

            FieldAsync<ProductStockType>("productStock",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IdGraphType>> {Name = "id"}),
                resolve: async context =>
                {
                    var id = context.GetArgument<int>("id");
                    var result = await contextServiceLocator.ProductStockRepository.Get(id);
                    return result;
                });

            Field<ProductType>("error",
                resolve: context =>
                {
                    context.Errors.Add(new ExecutionError("My unique execution error"));
                    return null;
                });
        }
    }
}

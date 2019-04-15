using company.inventory.data;
using company.inventory.data.Entities;
using company.inventory.data.Repositories;
using company.inventory.graph.Graph;
using company.inventory.graph.Graph.Types;
using company.inventory.graph.Services;
using GraphiQl;
using GraphQL;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace company.inventory.graph
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpContextAccessor();
            services.AddDbContext<InventoryContext>(options => options.UseInMemoryDatabase("Inventory"));

            //Service registration
            services.AddTransient<IRepository<Product>, ProductRepository>();
            services.AddTransient<IRepository<ProductStock>, ProductStockRepository>();
            services.AddTransient<IRepository<ProductService>, ProductServiceRepository>();
            services.AddTransient<ILookupById<ProductService>, ProductServiceRepository>();
            services.AddSingleton<ContextServiceLocator>();

            //Graph registration
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddSingleton<IDataLoaderContextAccessor, DataLoaderContextAccessor>();
            services.AddSingleton<DataLoaderDocumentListener>();
            services.AddSingleton<InventoryQuery>();
            services.AddSingleton<InventoryMutation>();
            services.AddSingleton<ProductType>();
            services.AddSingleton<ProductSearchInputType>();
            services.AddSingleton<ProductStockType>();
            services.AddSingleton<ProductServiceType>();
            services.AddSingleton<ProductUpdateInputType>();

            var serviceProvider = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new InventorySchema(new FuncDependencyResolver(type => serviceProvider.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl();
            }

            app.UseCors();
            app.UseMvc();
        }
    }
}

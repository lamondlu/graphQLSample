using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.GraphiQL;
using GraphQL.Server.Ui.Playground;
using GraphQL.Server.Ui.Voyager;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.Schemas;
using Orders.Services;

namespace Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IOrderService, OrderService>();
            services.AddSingleton<ICustomerService, CustomerService>();

            services.AddSingleton<OrderType>();
            services.AddSingleton<CustomerType>();
            services.AddSingleton<OrderStatusesEnum>();
            services.AddSingleton<OrdersQuery>();
            services.AddSingleton<OrdersSchema>();
            services.AddSingleton<OrderCreateInputType>();
            services.AddSingleton<OrdersMutation>();
            services.AddSingleton<IDependencyResolver>(c =>
                new FuncDependencyResolver(type => c.GetRequiredService(type)));


            services.AddGraphQL().AddWebSockets().AddDataLoader();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();

            // use websocket middleware for ChatSchema at path /graphql
            app.UseGraphQLWebSockets<OrdersSchema>("/graphql");

            // use HTTP middleware for ChatSchema at path /graphql
            app.UseGraphQL<OrdersSchema>("/graphql");

            app.UseGraphiQLServer(new GraphiQLOptions());

            // use graphql-playground middleware at default url /ui/playground
            app.UseGraphQLPlayground(new GraphQLPlaygroundOptions());

            // use voyager middleware at default url /ui/voyager
            app.UseGraphQLVoyager(new GraphQLVoyagerOptions());
        }
    }
}

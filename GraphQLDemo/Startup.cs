using GraphQLDemo.GraphQL;
using GraphQLDemo.GraphQL.Commands;
using GraphQLDemo.GraphQL.Platforms;
using GraphQLDemo.Infrastructure.Context;
using GraphQLDemo.Models.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using GraphQlVoyager = GraphQL.Server.Ui.Voyager;

namespace GraphQLDemo
{
    public class Startup
    {
        private const string SETTINGS_SECTION = "Settings";
        private readonly IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var settings = Configuration.GetSection(SETTINGS_SECTION).Get<ApiSettings>();
            services.AddSingleton(settings);

            services.AddPooledDbContextFactory<AppDbContext>(config =>
            {
                config.UseSqlServer(settings.SqlServerSettings.ConnectionString);
            });

            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddType<PlatformType>()
                .AddType<CommandType>()
                .AddFiltering()
                .AddSorting()
                .AddInMemorySubscriptions()
                .AddProjections();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });

            app.UseGraphQLVoyager(new GraphQlVoyager.GraphQLVoyagerOptions()
            {
                GraphQLEndPoint = "/graphql",
                Path = "/graphql-voyager"
            });
        }
    }
}

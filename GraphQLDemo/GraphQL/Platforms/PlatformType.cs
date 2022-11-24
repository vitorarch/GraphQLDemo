using GraphQLDemo.Infrastructure.Context;
using GraphQLDemo.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace GraphQLDemo.GraphQL.Platforms
{
    public class PlatformType : ObjectType<Platform>
    {
        protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
        {
            descriptor.Description("Some of most popular platforms used");

            descriptor.Field(p => p.Id)
                .Description("The Id of the platform.");

            descriptor.Field(p => p.Name)
                .Description("The name of the platform.");

            descriptor.Field(p => p.LicenseKey)
                .Description("The valid license key of the platform.");

            descriptor.Field(p => p.Commands)
                .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Useful commands for respective platform");
        }

        private class Resolvers
        {
            public IQueryable<Command> GetCommands([Parent] Platform platform, [ScopedService] AppDbContext appDbContext)
                => appDbContext.Command.Where(c => c.PlatformId.Equals(platform.Id));
        }
    }
}

using GraphQLDemo.Infrastructure.Context;
using GraphQLDemo.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace GraphQLDemo.GraphQL.Commands
{
    public class CommandType : ObjectType<Command>
    {
        protected override void Configure(IObjectTypeDescriptor<Command> descriptor)
        {
            descriptor.Description("Useful commands for they respectives paltforms");

            descriptor.Field(c => c.Id)
                .Description("The Id of the command.");

            descriptor.Field(c => c.HowTo)
                .Description("Description of command action.");

            descriptor.Field(c => c.CommandLine)
                .Description("The command line for respective action.");

            descriptor.Field(c => c.Platform)
                .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
                .UseDbContext<AppDbContext>()
                .Description("Platform belong");
        }

        private class Resolvers
        {
            public Platform GetPlatform([Parent] Command command, [ScopedService] AppDbContext appDbContext)
               => appDbContext.Platform.FirstOrDefault(p => p.Id.Equals(command.PlatformId));
        }
    }
}

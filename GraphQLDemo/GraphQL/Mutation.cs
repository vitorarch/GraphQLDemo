using GraphQLDemo.GraphQL.Commands;
using GraphQLDemo.GraphQL.Platforms;
using GraphQLDemo.Infrastructure.Context;
using GraphQLDemo.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLDemo.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input,
            [ScopedService] AppDbContext appDbContext,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = input.Name
            };

            appDbContext.Platform.Add(platform);
            await appDbContext.SaveChangesAsync(cancellationToken);

            await eventSender.SendAsync(
                nameof(Subscription.OnPlatformAdded),
                platform,
                cancellationToken);

            return new AddPlatformPayload(platform);
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(AddCommandInput input,
            [ScopedService] AppDbContext appDbContext)
        {
            var command = new Command
            {
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId,
            };

            appDbContext.Command.Add(command);
            await appDbContext.SaveChangesAsync();

            return new AddCommandPayload(command);
        }
    }
}

using GraphQLDemo.Models;
using HotChocolate;
using HotChocolate.Types;

namespace GraphQLDemo.GraphQL
{
    public class Subscription
    {

        [Subscribe]
        [Topic]
        public Platform OnPlatformAdded([EventMessage] Platform platform)
            => platform;
    }
}

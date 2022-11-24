using GraphQLDemo.Infrastructure.Context;
using GraphQLDemo.Models;
using HotChocolate;
using HotChocolate.Data;
using System.Linq;

namespace GraphQLDemo.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(AppDbContext))]
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Platform> GetPlatform([ScopedService] AppDbContext appDbContext)
        {
            return appDbContext.Platform;
        }


        [UseDbContext(typeof(AppDbContext))]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Command> GetCommand([ScopedService] AppDbContext appDbContext)
        {
            return appDbContext.Command;
        }
    }
}

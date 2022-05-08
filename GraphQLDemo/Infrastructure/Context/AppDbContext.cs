using GraphQLDemo.Infrastructure.Mappings;
using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlatformMapping());
            modelBuilder.ApplyConfiguration(new CommandMapping());
        }

        public DbSet<Platform> Platform { get; set; }
        public DbSet<Command> Command { get; set; }

    }
}

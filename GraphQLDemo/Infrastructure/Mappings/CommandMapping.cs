using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLDemo.Infrastructure.Mappings
{
    public class CommandMapping : IEntityTypeConfiguration<Command>
    {
        public void Configure(EntityTypeBuilder<Command> builder)
        {
            builder.ToTable("Command");

            builder.Property<int>("Id")
                 .HasColumnType("INTEGER")
                 .IsRequired();

            builder.Property<string>("HowTo")
                .HasColumnType("VARCHAR(30)")
                .IsRequired();

            builder.Property<string>("CommandLine")
                .HasColumnType("VARCHAR(30)")
                .HasMaxLength(50);

            builder.HasOne(c => c.Platform)
                .WithMany(c => c.Commands)
                .HasForeignKey(c => c.PlatformId);
        }
    }
}

using GraphQLDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GraphQLDemo.Infrastructure.Mappings
{
    public class PlatformMapping : IEntityTypeConfiguration<Platform>
    {
        public void Configure(EntityTypeBuilder<Platform> builder)
        {
            builder.ToTable("Platform");

            builder.Property<int>("Id")
                 .HasColumnType("INTEGER")
                 .IsRequired();

            builder.Property<string>("Name")
                .HasColumnType("VARCHAR(50)")
                .IsRequired();

            builder.Property<string>("LicenseKey")
                .HasColumnType("VARCHAR(50)")
                .HasMaxLength(50);

            builder.HasMany(p => p.Commands)
                .WithOne(p => p.Platform!)
                .HasForeignKey(p => p.PlatformId);
        }
    }
}

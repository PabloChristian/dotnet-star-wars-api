using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using StarWars.Domain.Entity;

namespace StarWars.Infrastructure.Data.Mapping
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(e => e.Username)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Password)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}

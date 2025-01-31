using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Infrastructure.Database.Mappings.Security;
public class RoleMapping : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.Property(role => role.Id)
                    .ValueGeneratedOnAdd()
                    .IsRequired();
        builder.HasKey(role => role.Id)
                    .HasName("PK_Role");
        builder.Property(role => role.Name)
                    .HasMaxLength(100)
                    .IsRequired();
        builder.HasIndex(role => role.Name)
                    .IsUnique();
        builder.Property(role => role.Description)
                    .HasMaxLength(255)
                    .IsRequired();
        builder.Property(role => role.CreatedDate)
                    .IsRequired()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Infrastructure.Database.Mappings.Security;
public class GroupRoleMapping : IEntityTypeConfiguration<GroupRole>
{
    public void Configure(EntityTypeBuilder<GroupRole> builder)
    {
        builder.ToTable("GroupRoles");
        builder.HasKey(groupRole => groupRole.Id);

        builder.Property(groupRole => groupRole.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(groupRole => groupRole.Description)
            .HasMaxLength(255);

        builder.HasMany(groupRole => groupRole.Roles)
            .WithMany()
            .UsingEntity<GroupRoleRole>(
                j => j
                    .HasOne(groupRoleRole => groupRoleRole.Role)
                    .WithMany()
                    .HasForeignKey(groupRoleRole => groupRoleRole.RoleId),
                j => j
                    .HasOne(groupRoleRole => groupRoleRole.GroupRole)
                    .WithMany()
                    .HasForeignKey(groupRoleRole => groupRoleRole.GroupRoleId),
                j =>
                {
                    j.HasKey(groupRoleRole => new { groupRoleRole.GroupRoleId, groupRoleRole.RoleId })
                        .HasName("PK_GroupRoleRole");
                    j.ToTable("GroupRoleRoles");
                });
    }
}
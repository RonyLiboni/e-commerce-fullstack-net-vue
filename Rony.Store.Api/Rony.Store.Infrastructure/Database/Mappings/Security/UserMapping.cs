using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Infrastructure.Database.Mappings.Security;
public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.Property(user => user.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
        builder.HasKey(user => user.Id)
                        .HasName("PK_User");
        builder.Property(user => user.FirstName)
                        .HasMaxLength(200)
                        .IsRequired();
        builder.Property(user => user.LastName)
                        .HasMaxLength(200)
                        .IsRequired();
        builder.Property(user => user.Email)
                        .HasMaxLength(200)
                        .IsRequired();
        builder.HasIndex(user => user.Email)
                       .IsUnique();
        builder.Property(user => user.Password)
                        .HasMaxLength(255)
                        .IsRequired();
        builder.Property(user => user.CreatedDate)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasMany(u => u.GroupRoles)
                    .WithMany()
                    .UsingEntity<UserGroupRole>(
                        j => j
                            .HasOne(userGroupRole => userGroupRole.GroupRole)
                            .WithMany()
                            .HasForeignKey(userGroupRole => userGroupRole.GroupRoleId),
                        j => j
                            .HasOne(userGroupRole => userGroupRole.User)
                            .WithMany()
                            .HasForeignKey(userGroupRole => userGroupRole.UserId),
                        j =>
                        {
                            j.HasKey(userGroupRole => new { userGroupRole.UserId, userGroupRole.GroupRoleId })
                            .HasName("PK_UserGroupRole");
                            j.ToTable("UserGroupRoles");
                        });
    }
}
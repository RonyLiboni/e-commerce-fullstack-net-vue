using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Security;

namespace Rony.Store.Infrastructure.Database.Mappings.Security;
public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.Property(refreshToken => refreshToken.Id)
                        .HasMaxLength(36)
                        .IsRequired();
        builder.HasKey(refreshToken => refreshToken.Id)
                        .HasName("PK_RefreshToken");
        builder.Property(refreshToken => refreshToken.Email)
                .HasMaxLength(255);
        builder.HasIndex(refreshToken => refreshToken.Email);
        builder.Property(refreshToken => refreshToken.ExpiresAt)
                        .IsRequired()
                        .HasColumnType("datetime2");
        builder.Property(refreshToken => refreshToken.CreatedAt)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
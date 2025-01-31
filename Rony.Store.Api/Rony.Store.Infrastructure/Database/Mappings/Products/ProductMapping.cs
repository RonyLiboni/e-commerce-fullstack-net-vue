using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Products;

namespace Rony.Store.Infrastructure.Database.Mappings.Products;
public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.Property(product => product.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
        builder.Property(product => product.Name)
                      .HasMaxLength(150);
        builder.Property(product => product.Sku)
                        .HasMaxLength(36)
                        .IsRequired();
        builder.Property(product => product.Price)
                      .HasPrecision(9, 2)
                      .IsRequired();
        builder.Property(product => product.Description)
                      .HasMaxLength(255);
        builder.Property(product => product.ImageKey)
                      .HasMaxLength(50);
        builder.Property(product => product.CreatedDate)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasKey(product => product.Id)
                        .HasName("PK_Product");
        builder.HasOne(product => product.Category);
        builder.HasIndex(product => product.Name)
                        .IsUnique();
        builder.HasIndex(product => product.Sku)
                        .IsUnique();
    }
}
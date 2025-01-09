using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Infrastructure.Database.Mappings.Departments;
public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        builder.Property(category => category.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
        builder.HasKey(category => category.Id)
                        .HasName("PK_Category");
        builder.HasOne(category => category.SubDepartment)
                        .WithMany(department => department.Categories)
                        .HasForeignKey(category => category.SubDepartmentId)
                        .HasConstraintName("FK_Category_SubDepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
        builder.Property(category => category.Name)
                        .HasMaxLength(50);
        builder.HasIndex(category => category.Name)
                        .IsUnique();
        builder.Property(category => category.CreatedDate)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}
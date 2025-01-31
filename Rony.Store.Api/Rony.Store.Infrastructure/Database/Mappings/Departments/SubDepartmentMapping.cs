using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Infrastructure.Database.Mappings.Departments;
public class SubDepartmentMapping : IEntityTypeConfiguration<SubDepartment>
{
    public void Configure(EntityTypeBuilder<SubDepartment> builder)
    {
        builder.ToTable("SubDepartments");
        builder.Property(subDepartment => subDepartment.Id)
                        .ValueGeneratedOnAdd()
                        .IsRequired();
        builder.HasKey(subDepartment => subDepartment.Id)
                        .HasName("PK_SubDepartment");
        builder.HasOne(subDepartment => subDepartment.Department)
                        .WithMany(department => department.SubDepartments)
                        .HasForeignKey(subDepartment => subDepartment.DepartmentId)
                        .HasConstraintName("FK_SubDepartment_DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(subDepartment => subDepartment.Categories)
                       .WithOne(category => category.SubDepartment)
                       .HasForeignKey(category => category.SubDepartmentId)
                       .OnDelete(DeleteBehavior.Cascade);
        builder.Property(subDepartment => subDepartment.Name)
                        .HasMaxLength(50);
        builder.HasIndex(subDepartment => subDepartment.Name)
                        .IsUnique();
        builder.Property(subDdepartment => subDdepartment.CreatedDate)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}

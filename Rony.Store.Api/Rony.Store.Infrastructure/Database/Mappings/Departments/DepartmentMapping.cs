using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Infrastructure.Database.Mappings.Departments;
public class DepartmentMapping : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");
        builder.Property(department => department.Id)
                    .ValueGeneratedOnAdd();
        builder.HasKey(department => department.Id)
                    .HasName("PK_Department");
        builder.HasMany(department => department.SubDepartments)
                    .WithOne(subDepartment => subDepartment.Department)
                    .HasForeignKey(subDepartment => subDepartment.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);
        builder.Property(department => department.Name)
                    .HasMaxLength(50);
        builder.HasIndex(department => department.Name)
                    .IsUnique();
        builder.Property(department => department.CreatedDate)
                    .IsRequired()
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}

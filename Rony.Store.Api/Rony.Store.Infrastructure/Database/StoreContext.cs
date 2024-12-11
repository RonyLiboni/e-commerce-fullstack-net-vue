using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Entities.Departments;

namespace Rony.Store.Infrastructure.Database;
public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(department =>
        {
            department.ToTable("Departments");
            department.Property(department => department.Id)
                        .ValueGeneratedOnAdd();
            department.HasKey(department => department.Id)
                        .HasName("PK_Department");
            department.HasMany(department => department.SubDepartments)
                        .WithOne(subDepartment => subDepartment.Department)
                        .HasForeignKey(subDepartment => subDepartment.DepartmentId)
                        .OnDelete(DeleteBehavior.Cascade);
            department.Property(department => department.Name)
                        .HasMaxLength(50);
            department.HasIndex(department => department.Name)
                        .IsUnique();
            department.Property(department => department.CreatedDate)
                        .IsRequired()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<SubDepartment>(subDepartment =>
        {
            subDepartment.ToTable("Sub_Departments");
            subDepartment.Property(subDepartment => subDepartment.Id)
                            .ValueGeneratedOnAdd()
                            .IsRequired();
            subDepartment.HasKey(subDepartment => subDepartment.Id)
                            .HasName("PK_Sub_Department");
            subDepartment.HasOne(subDepartment => subDepartment.Department)
                            .WithMany(department => department.SubDepartments)
                            .HasForeignKey(subDepartment => subDepartment.DepartmentId)
                            .HasConstraintName("FK_Sub_Department_Department_Id")
                            .OnDelete(DeleteBehavior.Cascade);
            subDepartment.HasMany(subDepartment => subDepartment.Categories)
                           .WithOne(category => category.SubDepartment)
                           .HasForeignKey(category => category.SubDepartmentId)
                           .OnDelete(DeleteBehavior.Cascade);
            subDepartment.Property(subDepartment => subDepartment.Name)
                            .HasMaxLength(50);
            subDepartment.HasIndex(subDepartment => subDepartment.Name)
                            .IsUnique();
            subDepartment.Property(subDdepartment => subDdepartment.CreatedDate)
                            .IsRequired()
                            .HasColumnType("datetime2")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Category>(category =>
        {
            category.ToTable("Categories");
            category.Property(category => category.Id)
                            .ValueGeneratedOnAdd()
                            .IsRequired();
            category.HasKey(category => category.Id)
                            .HasName("PK_Category");
            category.HasOne(category => category.SubDepartment)
                            .WithMany(department => department.Categories)
                            .HasForeignKey(category => category.SubDepartmentId)
                            .HasConstraintName("FK_Category_Sub_Department_Id")
                            .OnDelete(DeleteBehavior.Cascade);
            category.Property(category => category.Name)
                            .HasMaxLength(50);
            category.HasIndex(category => category.Name)
                            .IsUnique();
            category.Property(subDdepartment => subDdepartment.CreatedDate)
                            .IsRequired()
                            .HasColumnType("datetime2")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

    }
}

using Microsoft.EntityFrameworkCore;
using Rony.Store.Domain.Entities.Departments;
using Rony.Store.Domain.Entities.Products;

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
            subDepartment.ToTable("SubDepartments");
            subDepartment.Property(subDepartment => subDepartment.Id)
                            .ValueGeneratedOnAdd()
                            .IsRequired();
            subDepartment.HasKey(subDepartment => subDepartment.Id)
                            .HasName("PK_SubDepartment");
            subDepartment.HasOne(subDepartment => subDepartment.Department)
                            .WithMany(department => department.SubDepartments)
                            .HasForeignKey(subDepartment => subDepartment.DepartmentId)
                            .HasConstraintName("FK_SubDepartment_DepartmentId")
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
                            .HasConstraintName("FK_Category_SubDepartmentId")
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

        modelBuilder.Entity<Product>(product =>
        {
            product.ToTable("Products");
            product.Property(product => product.Id)
                            .ValueGeneratedOnAdd()
                            .IsRequired();
            product.Property(product => product.Name)
                          .HasMaxLength(150);
            product.Property(product => product.Sku)
                            .HasMaxLength(36)
                            .IsRequired();
            product.Property(product => product.Price)
                          .HasPrecision(9, 2)
                          .IsRequired();
            product.Property(product => product.Description)
                          .HasMaxLength(255);
            product.Property(product => product.ImageKey)
                          .HasMaxLength(50);
            product.Property(product => product.CreatedDate)
                            .IsRequired()
                            .HasColumnType("datetime2")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP");
            product.HasKey(product => product.Id)
                            .HasName("PK_Product");
            product.HasOne(product => product.Category);
            product.HasIndex(product => product.Name)
                            .IsUnique();
            product.HasIndex(product => product.Sku)
                            .IsUnique();
        });

    }
}

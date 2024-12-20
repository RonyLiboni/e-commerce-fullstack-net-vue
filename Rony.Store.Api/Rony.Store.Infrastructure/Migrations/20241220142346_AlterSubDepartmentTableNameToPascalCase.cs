using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rony.Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterSubDepartmentTableNameToPascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Sub_Departments",
                newName: "SubDepartments");

            migrationBuilder.RenameIndex(
                name: "IX_Sub_Departments_Name",
                table: "SubDepartments",
                newName: "IX_SubDepartments_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Sub_Departments_DepartmentId",
                table: "SubDepartments",
                newName: "IX_SubDepartments_DepartmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "SubDepartments",
                newName: "Sub_Departments");

            migrationBuilder.RenameIndex(
                name: "IX_SubDepartments_Name",
                table: "Sub_Departments",
                newName: "IX_Sub_Departments_Name");

            migrationBuilder.RenameIndex(
                name: "IX_SubDepartments_DepartmentId",
                table: "Sub_Departments",
                newName: "IX_Sub_Departments_DepartmentId");
        }
    }
}

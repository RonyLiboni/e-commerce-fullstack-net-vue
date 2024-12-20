using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rony.Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterConstraintNamesToPascalCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Sub_Department_Id",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Sub_Department_Department_Id",
                table: "SubDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sub_Department",
                table: "SubDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubDepartment",
                table: "SubDepartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_SubDepartmentId",
                table: "Categories",
                column: "SubDepartmentId",
                principalTable: "SubDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SubDepartment_DepartmentId",
                table: "SubDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_SubDepartmentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDepartment_DepartmentId",
                table: "SubDepartments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubDepartment",
                table: "SubDepartments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sub_Department",
                table: "SubDepartments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Sub_Department_Id",
                table: "Categories",
                column: "SubDepartmentId",
                principalTable: "SubDepartments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sub_Department_Department_Id",
                table: "SubDepartments",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

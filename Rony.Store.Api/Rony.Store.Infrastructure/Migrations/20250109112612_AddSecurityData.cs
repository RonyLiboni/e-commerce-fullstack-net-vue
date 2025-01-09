using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rony.Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSecurityData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Email", "Password", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Ronald", "Liboni", "ronaldliboni@gmail.com", "$2a$11$FJEgxnlzuQMjG2fH05ZtxORnI4hXQs7YYY6b3EinAyw5lcm7bg18O", DateTime.UtcNow },
                    { 2, "John", "Doe", "user@example.com", "$2a$11$FJEgxnlzuQMjG2fH05ZtxORnI4hXQs7YYY6b3EinAyw5lcm7bg18O", DateTime.UtcNow }
                });

            migrationBuilder.InsertData(
                table: "GroupRoles",
                columns: new[] { "Id", "Description", "Name", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "This group has full access to all system features and functionalities.", "Master", DateTime.UtcNow },
                    { 2, "This group can only perform read operations", "MasterRead", DateTime.UtcNow }
                });

            migrationBuilder.InsertData(
                table: "UserGroupRoles",
                columns: new[] { "UserId", "GroupRoleId", },
                values: new object[,]
                {
                    { 1 , 1 },
                    { 2 , 2 },
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Name", "CreatedDate" },
                values: new object[,]
                {
                    { 17, "Find Departments by parameters", "Department-Find", DateTime.UtcNow },
                    { 1, "Find a Department by its ID.", "Department-FindById", DateTime.UtcNow },
                    { 2, "Update a Department by its ID.", "Department-UpdateById", DateTime.UtcNow },
                    { 3, "Create a new Department.", "Department-Create", DateTime.UtcNow },

                    { 4, "Find a SubDepartment by its ID.", "SubDepartment-FindById", DateTime.UtcNow },
                    { 5, "Update a SubDepartment by its ID.", "SubDepartment-UpdateById", DateTime.UtcNow },
                    { 6, "Create a new SubDepartment.", "SubDepartment-Create", DateTime.UtcNow },

                    { 18, "Find all Categories", "Category-FindAll", DateTime.UtcNow },
                    { 7, "Find a Category by its ID.", "Category-FindById", DateTime.UtcNow },
                    { 8, "Update a Category by its ID.", "Category-UpdateById", DateTime.UtcNow },
                    { 9, "Create a new Category.", "Category-Create", DateTime.UtcNow },

                    { 16, "Find Products by parameters", "Product-Find", DateTime.UtcNow },
                    { 10, "Find a Product by its ID.", "Product-FindById", DateTime.UtcNow },
                    { 11, "Update a Product by its ID.", "Product-UpdateById", DateTime.UtcNow },
                    { 12, "Create a new Product.", "Product-Create", DateTime.UtcNow },

                    { 13, "Find a User by its ID.", "User-FindById", DateTime.UtcNow },
                    { 14, "Update a User by its ID.", "User-UpdateById", DateTime.UtcNow },
                    { 15, "Create a new User.", "User-Create", DateTime.UtcNow }
                });

            migrationBuilder.InsertData(
                table: "GroupRoleRoles",
                columns: new[] { "RoleId", "GroupRoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 5, 1 },
                    { 6, 1 },
                    { 7, 1 },
                    { 8, 1 },
                    { 9, 1 },
                    { 10, 1 },
                    { 11, 1 },
                    { 12, 1 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 18, 1 },
                    { 1, 2 },
                    { 4, 2 },
                    { 7, 2 },
                    { 10, 2 },
                    { 13, 2 },
                    { 16, 2 },
                    { 17, 2 },
                    { 18, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

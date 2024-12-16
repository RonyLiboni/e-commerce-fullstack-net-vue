using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rony.Store.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProductAndDepartmentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Computers", DateTime.UtcNow },
                    { 2, "Games", DateTime.UtcNow },
                });

            migrationBuilder.InsertData(
                table: "Sub_Departments",
                columns: new[] { "Id", "Name","DepartmentId", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Laptops",1, DateTime.UtcNow },
                    { 3, "Nintendo",2, DateTime.UtcNow },
                    { 4, "Playstation",2, DateTime.UtcNow },
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "SubDepartmentId", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Dell",1, DateTime.UtcNow },
                    { 2, "Macbook",1, DateTime.UtcNow },
                    { 4, "Nintendo Consoles",3, DateTime.UtcNow },
                    { 5, "Controllers for Nintendo",3, DateTime.UtcNow },
                    { 6, "Games for Nintendo",3, DateTime.UtcNow },
                    { 7, "Playstation Consoles",4, DateTime.UtcNow },
                    { 8, "Controllers for Playstation",4, DateTime.UtcNow },
                    { 9, "Games for Playstation",4, DateTime.UtcNow },
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Name","Sku", "Price", "Description","CategoryId","ImageKey", "CreatedDate" },
                values: new object[,]
                {
                    { 1, "Sony - PlayStation 5 Slim Console Digital Edition - Fortnite Cobalt Star Bundle - White","SP5SCDE", 374.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",7,"1.jpg",DateTime.UtcNow },
                    { 2, "Sony - PlayStation 5 Slim Console - White","SP5SCDEW", 424.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",7,"2.jpg",DateTime.UtcNow },
                    { 3, "Marvel's Spider-Man 2 Standard Edition - PlayStation 5","SP5SCDEWSPIDER", 39.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",9,"3.jpg",DateTime.UtcNow },
                    { 4, "God of War Ragnarök Standard Edition - PlayStation 5","SP5SCDEWGOWR", 424.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",9,"4.jpg",DateTime.UtcNow },
                    { 5, "Sony - PlayStation 5 - DualSense Wireless Controller - White","SP5SCDEWCON", 74.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",8,"5.jpg",DateTime.UtcNow },
                    { 6, "Nintendo Switch: Mario Kart 8 Deluxe Bundle - Multi","NINSWMK", 299.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",4,"6.jpg",DateTime.UtcNow },
                    { 7, "Nintendo - Joy-Con (L/R) Wireless Controllers - Pastel Purple/ Pastel Green","NINSWMKCON", 74.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",5,"7.jpg",DateTime.UtcNow },
                    { 8, "The Legend of Zelda: Breath of the Wild - Nintendo Switch – OLED Model, Nintendo Switch, Nintendo Switch Lite","NINSZELD", 59.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",6,"8.jpg",DateTime.UtcNow },
                    { 9, "Minecraft Standard Edition - Nintendo Switch – OLED Model, Nintendo Switch, Nintendo Switch Lite","NINSWGAMIN", 29.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",6,"9.jpg",DateTime.UtcNow },
                    { 10, "Apple - MacBook Air 13-inch Apple M2 chip Built for Apple Intelligence - 16GB Memory - 256GB SSD - Midnight","MACAPPLE13M2", 799m,"Lorem ipsum consectetur suspendisse habitasse porta.",2,"10.jpg",DateTime.UtcNow },
                    { 11, "Apple - MacBook Pro 14-inch Apple M4 chip Built for Apple Intelligence - 16GB Memory - 512GB SSD - Space Black","MACAPPLE14512", 1499m,"Lorem ipsum consectetur suspendisse habitasse porta.",2,"11.jpg",DateTime.UtcNow },
                    { 12, "Dell - Inspiron 2-in-1 14” IPS LED FHD+ Touch Screen Laptop – Intel Core 5 with 8GB Memory – 512GB SSD - Ice Blue","DELLNOTE8512", 749.99m,"Lorem ipsum consectetur suspendisse habitasse porta.",1,"12.jpg",DateTime.UtcNow },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

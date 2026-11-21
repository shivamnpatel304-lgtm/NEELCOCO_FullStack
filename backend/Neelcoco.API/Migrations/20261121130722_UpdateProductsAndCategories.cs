using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Neelcoco.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductsAndCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Ice Pops", "ice-pops" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "MRP", "Name", "Price", "Slug" },
                values: new object[] { "Refreshing and tangy blueberry ice gola pop bursting with chilled berry coolness.", "/blueberry.jpeg", 25m, "Blueberry Ice Pop", 20m, "blueberry-ice-pop" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "MRP", "Name", "Price", "Slug", "StockQuantity", "Unit" },
                values: new object[] { 1, "Signature rich cold cocoa milk indulgence frozen into a delicious milky pop.", "/coldcoco.jpeg", 30m, "Cold Coco Milky Pop", 25m, "cold-coco-milky-pop", 500, "1 pc" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "MRP", "Name", "Price", "Slug", "StockQuantity" },
                values: new object[] { 1, "Tangy and refreshing kachi keri raw mango ice gola pop with a zesty burst.", "/green.jpeg", 25m, "Raw Mango Ice Pop", 20m, "raw-mango-ice-pop", 500 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsFeatured", "MRP", "Name", "Price", "Slug", "StockQuantity", "Unit" },
                values: new object[] { 1, "Classic sweet and tangy spiced jamun-flavour Kala Khatta ice gola pop.", "/kotha.jpeg", true, 25m, "Kala Khatta Ice Pop", 20m, "kala-khatta-ice-pop", 500, "1 pc" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "IsActive", "IsFeatured", "MRP", "Name", "Price", "Slug", "StockQuantity", "Unit" },
                values: new object[,]
                {
                    { 5, 1, "Luscious alphonso mango combined with rich creamy milk in an ice pop.", "/mango.jpeg", true, true, 30m, "Mango Milky Pop", 25m, "mango-milky-pop", 500, "1 pc" },
                    { 6, 1, "Vibrant citrus burst of chilled orange ice gola pop for total refreshment.", "/orange.jpeg", true, true, 25m, "Orange Ice Pop", 20m, "orange-ice-pop", 500, "1 pc" },
                    { 7, 1, "Royal saffron-infused rich milk ice pop with traditional rich Indian flavours.", "/rajbhog.jpeg", true, true, 30m, "Rajbhog Milky Pop", 25m, "rajbhog-milky-pop", 500, "1 pc" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Name", "Slug" },
                values: new object[] { "Kulfi", "kulfi" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Slug" },
                values: new object[,]
                {
                    { 2, "Ice Pops", "ice-pops" },
                    { 3, "Dairy Desserts", "dairy-desserts" },
                    { 4, "Mukhwas", "mukhwas" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "ImageUrl", "MRP", "Name", "Price", "Slug" },
                values: new object[] { "Creamy traditional milk kulfi.", "https://images.unsplash.com/photo-1563805042-7684c019e1cb?auto=format&fit=crop&w=900&q=80", 50m, "Malai Kulfi", 40m, "malai-kulfi" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "MRP", "Name", "Price", "Slug", "StockQuantity", "Unit" },
                values: new object[] { 3, "Rich chocolate dairy dessert.", "https://images.unsplash.com/photo-1575377427642-087cf684f29d?auto=format&fit=crop&w=900&q=80", 40m, "Chocolate Splash", 35m, "chocolate-splash", 400, "200 ml" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "MRP", "Name", "Price", "Slug", "StockQuantity" },
                values: new object[] { 2, "Fun creamy frozen treat.", "https://images.unsplash.com/photo-1570197788417-0e82375c9371?auto=format&fit=crop&w=900&q=80", 20m, "Milky Pop", 15m, "milky-pop", 1000 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoryId", "Description", "ImageUrl", "IsFeatured", "MRP", "Name", "Price", "Slug", "StockQuantity", "Unit" },
                values: new object[] { 4, "Aromatic mouth freshener inspired by Indian ingredients.", "https://images.unsplash.com/photo-1601050690597-df0568f70950?auto=format&fit=crop&w=900&q=80", false, 100m, "Jamun Seed Mukhwas", 80m, "jamun-seed-mukhwas", 300, "100 g" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("472534e9-f33b-4b78-ab86-07e3acdc9482"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("5f6469d1-f03e-4deb-9885-a262bf3b0563"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("78d9177e-312f-4aa4-ae68-c5940cb4d726"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("91a00f3c-b2f7-4b97-a4a9-1ec30de96de0"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("aff42dd5-56cc-49e3-b94d-fcc728b8854d"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("209faec1-e779-46d6-9fda-6de24e9e6575"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("aec2c22e-fab5-438b-a325-1a2af3dae381"));

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    HouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Houses_HouseId",
                        column: x => x.HouseId,
                        principalTable: "Houses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("17696d6a-529e-4b46-b88c-0cdffc98fe9a"), new DateTime(2025, 12, 5, 1, 23, 15, 476, DateTimeKind.Utc).AddTicks(1719), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" },
                    { new Guid("2fef9259-2d0c-41c6-873f-37188d87a386"), new DateTime(2025, 12, 5, 1, 23, 15, 476, DateTimeKind.Utc).AddTicks(1751), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("bb97e7f2-9ab6-4e55-b29f-828b046d5c32"), "Kaş, Antalya", new Guid("17696d6a-529e-4b46-b88c-0cdffc98fe9a"), "Antalya", "villa1.jpg", new DateTime(2025, 12, 5, 1, 23, 15, 476, DateTimeKind.Utc).AddTicks(1757), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" },
                    { new Guid("deaa919b-33f9-4b4c-be74-317bfca22cb5"), "Şişli, İstanbul", new Guid("2fef9259-2d0c-41c6-873f-37188d87a386"), "İstanbul", "daire1.jpg", new DateTime(2025, 12, 5, 1, 23, 15, 476, DateTimeKind.Utc).AddTicks(1763), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("0eb9841f-3404-4db8-a2cb-3f3b7a3e773b"), new Guid("bb97e7f2-9ab6-4e55-b29f-828b046d5c32"), "villa1_2.jpg" },
                    { new Guid("aace6701-0332-4dcc-973f-9af1ac51c131"), new Guid("bb97e7f2-9ab6-4e55-b29f-828b046d5c32"), "villa1_1.jpg" },
                    { new Guid("eb48a13d-8869-45b7-a0ce-56d706b9b1aa"), new Guid("deaa919b-33f9-4b4c-be74-317bfca22cb5"), "daire1_1.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_HouseId",
                table: "Reviews",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("0eb9841f-3404-4db8-a2cb-3f3b7a3e773b"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("aace6701-0332-4dcc-973f-9af1ac51c131"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("eb48a13d-8869-45b7-a0ce-56d706b9b1aa"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("bb97e7f2-9ab6-4e55-b29f-828b046d5c32"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("deaa919b-33f9-4b4c-be74-317bfca22cb5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("17696d6a-529e-4b46-b88c-0cdffc98fe9a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("2fef9259-2d0c-41c6-873f-37188d87a386"));

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("209faec1-e779-46d6-9fda-6de24e9e6575"), new DateTime(2025, 11, 12, 11, 29, 48, 184, DateTimeKind.Utc).AddTicks(9618), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" },
                    { new Guid("aec2c22e-fab5-438b-a325-1a2af3dae381"), new DateTime(2025, 11, 12, 11, 29, 48, 184, DateTimeKind.Utc).AddTicks(9568), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("91a00f3c-b2f7-4b97-a4a9-1ec30de96de0"), "Şişli, İstanbul", new Guid("209faec1-e779-46d6-9fda-6de24e9e6575"), "İstanbul", "daire1.jpg", new DateTime(2025, 11, 12, 11, 29, 48, 184, DateTimeKind.Utc).AddTicks(9639), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" },
                    { new Guid("aff42dd5-56cc-49e3-b94d-fcc728b8854d"), "Kaş, Antalya", new Guid("aec2c22e-fab5-438b-a325-1a2af3dae381"), "Antalya", "villa1.jpg", new DateTime(2025, 11, 12, 11, 29, 48, 184, DateTimeKind.Utc).AddTicks(9626), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("472534e9-f33b-4b78-ab86-07e3acdc9482"), new Guid("aff42dd5-56cc-49e3-b94d-fcc728b8854d"), "villa1_2.jpg" },
                    { new Guid("5f6469d1-f03e-4deb-9885-a262bf3b0563"), new Guid("aff42dd5-56cc-49e3-b94d-fcc728b8854d"), "villa1_1.jpg" },
                    { new Guid("78d9177e-312f-4aa4-ae68-c5940cb4d726"), new Guid("91a00f3c-b2f7-4b97-a4a9-1ec30de96de0"), "daire1_1.jpg" }
                });
        }
    }
}

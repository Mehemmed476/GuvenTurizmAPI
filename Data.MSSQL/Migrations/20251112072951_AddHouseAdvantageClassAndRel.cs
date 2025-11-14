using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddHouseAdvantageClassAndRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("0d45fcc2-3d4a-488d-bb29-f8a04bf11d37"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("6939bdc4-6f5a-4b73-b7d6-a1dbea1e417e"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("dcc4caa5-0f6e-4a34-88a5-fee087818f20"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("b2a1e958-f83f-44d7-9a53-e9e42029eac4"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("b39e4374-2685-49d4-8d1e-9f65a4c58a3a"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("039b2872-6318-4ebd-86e7-02925f48d7da"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4f356c9c-e621-42d9-9e16-e549365d4de9"));

            migrationBuilder.DropColumn(
                name: "HouseAdvantages",
                table: "Houses");

            migrationBuilder.CreateTable(
                name: "HouseAdvantages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseAdvantages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HouseHouseAdvantageRels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HouseAdvantageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HouseHouseAdvantageRels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HouseHouseAdvantageRels_HouseAdvantages_HouseAdvantageId",
                        column: x => x.HouseAdvantageId,
                        principalTable: "HouseAdvantages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HouseHouseAdvantageRels_Houses_HouseId",
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

            migrationBuilder.CreateIndex(
                name: "IX_HouseHouseAdvantageRels_HouseAdvantageId",
                table: "HouseHouseAdvantageRels",
                column: "HouseAdvantageId");

            migrationBuilder.CreateIndex(
                name: "IX_HouseHouseAdvantageRels_HouseId",
                table: "HouseHouseAdvantageRels",
                column: "HouseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HouseHouseAdvantageRels");

            migrationBuilder.DropTable(
                name: "HouseAdvantages");

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

            migrationBuilder.AddColumn<int>(
                name: "HouseAdvantages",
                table: "Houses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("039b2872-6318-4ebd-86e7-02925f48d7da"), new DateTime(2025, 11, 9, 16, 17, 32, 255, DateTimeKind.Utc).AddTicks(2659), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" },
                    { new Guid("4f356c9c-e621-42d9-9e16-e549365d4de9"), new DateTime(2025, 11, 9, 16, 17, 32, 255, DateTimeKind.Utc).AddTicks(2632), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "HouseAdvantages", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("b2a1e958-f83f-44d7-9a53-e9e42029eac4"), "Kaş, Antalya", new Guid("4f356c9c-e621-42d9-9e16-e549365d4de9"), "Antalya", "villa1.jpg", new DateTime(2025, 11, 9, 16, 17, 32, 255, DateTimeKind.Utc).AddTicks(2664), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", 552, null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" },
                    { new Guid("b39e4374-2685-49d4-8d1e-9f65a4c58a3a"), "Şişli, İstanbul", new Guid("039b2872-6318-4ebd-86e7-02925f48d7da"), "İstanbul", "daire1.jpg", new DateTime(2025, 11, 9, 16, 17, 32, 255, DateTimeKind.Utc).AddTicks(2670), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", 528, null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("0d45fcc2-3d4a-488d-bb29-f8a04bf11d37"), new Guid("b2a1e958-f83f-44d7-9a53-e9e42029eac4"), "villa1_2.jpg" },
                    { new Guid("6939bdc4-6f5a-4b73-b7d6-a1dbea1e417e"), new Guid("b39e4374-2685-49d4-8d1e-9f65a4c58a3a"), "daire1_1.jpg" },
                    { new Guid("dcc4caa5-0f6e-4a34-88a5-fee087818f20"), new Guid("b2a1e958-f83f-44d7-9a53-e9e42029eac4"), "villa1_1.jpg" }
                });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddFAQandSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "FAQs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_FAQs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("39ab03f1-a10e-4a77-8d72-72265ae05991"), new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6948), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" },
                    { new Guid("eea3f0e7-1c5a-4727-b814-3011a7a0e24d"), new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6951), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" }
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "Id", "Answer", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DisplayOrder", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "Question" },
                values: new object[,]
                {
                    { new Guid("893370d0-4f26-4870-bb43-e6074969699d"), "Bəli, evlərimizin 90%-i sürətli internetlə təmin olunub.", new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6934), null, null, null, 1, true, false, null, null, "Evlərdə Wi-Fi var?" },
                    { new Guid("b7a010b1-18cd-4588-937d-d77cf20001b1"), "Giriş 14:00, Çıxış 12:00-dır.", new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6942), null, null, null, 3, true, false, null, null, "Giriş və Çıxış saatları neçədir?" },
                    { new Guid("c8abaca6-c3fb-41c4-9630-0563609290e4"), "Saytımızdan bəyəndiyiniz evi seçib 'Bron et' düyməsinə basaraq.", new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6940), null, null, null, 2, true, false, null, null, "Necə rezervasiya edə bilərəm?" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "IsDeleted", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("19656215-08f9-4c07-8cd1-ed624273d2fc"), false, "PhoneNumber", "+994 50 123 45 67" },
                    { new Guid("41ce62ad-3b62-4d4f-8d21-780f84ed519f"), false, "Address", "H. Əliyev pr., Quba, Azərbaycan" },
                    { new Guid("870f1b9b-6948-4107-990d-556d462ed775"), false, "Email", "info@guventurizm.az" },
                    { new Guid("98f1708e-002f-44fc-bf1e-40e2a85fe1e4"), false, "Copyright", "© 2025 Güvən Turizm. Bütün hüquqlar qorunur." },
                    { new Guid("b458ccb6-a9f3-43a5-836d-395a9893ba5b"), false, "Instagram", "https://instagram.com/guventurizm" },
                    { new Guid("bf22b336-47bf-4406-9938-214e453e49e9"), false, "Whatsapp", "https://wa.me/994501234567" },
                    { new Guid("d5caedb6-8baf-489a-a22c-3914086c4c4f"), false, "Facebook", "https://facebook.com/guventurizm" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("327bb7f6-3fff-433b-937f-af1b2f983c69"), "Kaş, Antalya", new Guid("39ab03f1-a10e-4a77-8d72-72265ae05991"), "Antalya", "villa1.jpg", new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6954), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" },
                    { new Guid("f3f3f8af-2c41-4a82-944a-3a9d32a25b5f"), "Şişli, İstanbul", new Guid("eea3f0e7-1c5a-4727-b814-3011a7a0e24d"), "İstanbul", "daire1.jpg", new DateTime(2025, 12, 5, 2, 56, 33, 114, DateTimeKind.Utc).AddTicks(6960), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("65e16794-ad71-44f7-bb90-7ebe554e6863"), new Guid("f3f3f8af-2c41-4a82-944a-3a9d32a25b5f"), "daire1_1.jpg" },
                    { new Guid("a4f17607-af61-440b-bd9e-032c237361ec"), new Guid("327bb7f6-3fff-433b-937f-af1b2f983c69"), "villa1_1.jpg" },
                    { new Guid("b5b54091-325f-41a3-b6fd-8ba3452661a9"), new Guid("327bb7f6-3fff-433b-937f-af1b2f983c69"), "villa1_2.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQs");

            migrationBuilder.DropTable(
                name: "Settings");

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("65e16794-ad71-44f7-bb90-7ebe554e6863"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("a4f17607-af61-440b-bd9e-032c237361ec"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("b5b54091-325f-41a3-b6fd-8ba3452661a9"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("327bb7f6-3fff-433b-937f-af1b2f983c69"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("f3f3f8af-2c41-4a82-944a-3a9d32a25b5f"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("39ab03f1-a10e-4a77-8d72-72265ae05991"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("eea3f0e7-1c5a-4727-b814-3011a7a0e24d"));

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
        }
    }
}

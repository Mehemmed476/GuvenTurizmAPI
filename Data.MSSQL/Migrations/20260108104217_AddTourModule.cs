using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddTourModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("893370d0-4f26-4870-bb43-e6074969699d"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("b7a010b1-18cd-4588-937d-d77cf20001b1"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("c8abaca6-c3fb-41c4-9630-0563609290e4"));

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
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("19656215-08f9-4c07-8cd1-ed624273d2fc"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("41ce62ad-3b62-4d4f-8d21-780f84ed519f"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("870f1b9b-6948-4107-990d-556d462ed775"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("98f1708e-002f-44fc-bf1e-40e2a85fe1e4"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("b458ccb6-a9f3-43a5-836d-395a9893ba5b"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("bf22b336-47bf-4406-9938-214e453e49e9"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("d5caedb6-8baf-489a-a22c-3914086c4c4f"));

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

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationDay = table.Column<int>(type: "int", nullable: false),
                    DurationNight = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_Tours", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TourFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: false),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourFiles_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPackages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PackageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscountPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPackages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPackages_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourPackageInclusions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIncluded = table.Column<bool>(type: "bit", nullable: false),
                    TourPackageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourPackageInclusions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TourPackageInclusions_TourPackages_TourPackageId",
                        column: x => x.TourPackageId,
                        principalTable: "TourPackages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("5101870e-77a6-4ca2-ba94-8bb23b2454fc"), new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2335), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" },
                    { new Guid("8d4dcfcc-f52e-4182-83d6-723d8b74f132"), new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2333), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" }
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "Id", "Answer", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DisplayOrder", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "Question" },
                values: new object[,]
                {
                    { new Guid("4ff9dd04-016b-4033-a874-3f50c62720d6"), "Giriş 14:00, Çıxış 12:00-dır.", new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2325), null, null, null, 3, true, false, null, null, "Giriş və Çıxış saatları neçədir?" },
                    { new Guid("a0646104-5878-4b66-8ea2-ea27f320b58e"), "Bəli, evlərimizin 90%-i sürətli internetlə təmin olunub.", new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2315), null, null, null, 1, true, false, null, null, "Evlərdə Wi-Fi var?" },
                    { new Guid("f4ef4a54-4a79-4001-9401-0dffbb495997"), "Saytımızdan bəyəndiyiniz evi seçib 'Bron et' düyməsinə basaraq.", new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2321), null, null, null, 2, true, false, null, null, "Necə rezervasiya edə bilərəm?" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "IsDeleted", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("0add27c0-0c3a-4ae1-9be3-a21e52fd29e9"), false, "Email", "info@guventurizm.az" },
                    { new Guid("2dfc2837-9601-4182-a828-383fad52a077"), false, "Address", "H. Əliyev pr., Quba, Azərbaycan" },
                    { new Guid("6fc632a1-030b-4a4f-a476-ba39c1b5b101"), false, "Facebook", "https://facebook.com/guventurizm" },
                    { new Guid("72a22a6a-ed75-48bc-8e87-a355ebbcd6fb"), false, "Copyright", "© 2025 Güvən Turizm. Bütün hüquqlar qorunur." },
                    { new Guid("976c97be-17c5-4942-8fe3-5ddb0c40e14b"), false, "Whatsapp", "https://wa.me/994501234567" },
                    { new Guid("be7f5c50-1646-4b4c-beb0-5d2ac6dc8679"), false, "Instagram", "https://instagram.com/guventurizm" },
                    { new Guid("e2eb0f31-1e17-49e9-9d8e-a245b7d59df7"), false, "PhoneNumber", "+994 50 123 45 67" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("8d2d23f8-0bb2-4a9c-a446-2e07e90df1aa"), "Şişli, İstanbul", new Guid("5101870e-77a6-4ca2-ba94-8bb23b2454fc"), "İstanbul", "daire1.jpg", new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2346), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" },
                    { new Guid("923e7632-ced4-4f3f-ab21-99d8d57a4810"), "Kaş, Antalya", new Guid("8d4dcfcc-f52e-4182-83d6-723d8b74f132"), "Antalya", "villa1.jpg", new DateTime(2026, 1, 8, 14, 42, 17, 280, DateTimeKind.Utc).AddTicks(2339), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("4088bce8-7641-4226-b2bd-726c844d340b"), new Guid("923e7632-ced4-4f3f-ab21-99d8d57a4810"), "villa1_1.jpg" },
                    { new Guid("4eaad948-14f1-420d-a3e3-4e176c4b3ae8"), new Guid("8d2d23f8-0bb2-4a9c-a446-2e07e90df1aa"), "daire1_1.jpg" },
                    { new Guid("6b5446a2-907b-46b9-bbe7-f98ad93953cb"), new Guid("923e7632-ced4-4f3f-ab21-99d8d57a4810"), "villa1_2.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TourFiles_TourId",
                table: "TourFiles",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPackageInclusions_TourPackageId",
                table: "TourPackageInclusions",
                column: "TourPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_TourPackages_TourId",
                table: "TourPackages",
                column: "TourId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourFiles");

            migrationBuilder.DropTable(
                name: "TourPackageInclusions");

            migrationBuilder.DropTable(
                name: "TourPackages");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("4ff9dd04-016b-4033-a874-3f50c62720d6"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("a0646104-5878-4b66-8ea2-ea27f320b58e"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("f4ef4a54-4a79-4001-9401-0dffbb495997"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("4088bce8-7641-4226-b2bd-726c844d340b"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("4eaad948-14f1-420d-a3e3-4e176c4b3ae8"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("6b5446a2-907b-46b9-bbe7-f98ad93953cb"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("0add27c0-0c3a-4ae1-9be3-a21e52fd29e9"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("2dfc2837-9601-4182-a828-383fad52a077"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("6fc632a1-030b-4a4f-a476-ba39c1b5b101"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("72a22a6a-ed75-48bc-8e87-a355ebbcd6fb"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("976c97be-17c5-4942-8fe3-5ddb0c40e14b"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("be7f5c50-1646-4b4c-beb0-5d2ac6dc8679"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("e2eb0f31-1e17-49e9-9d8e-a245b7d59df7"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("8d2d23f8-0bb2-4a9c-a446-2e07e90df1aa"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("923e7632-ced4-4f3f-ab21-99d8d57a4810"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5101870e-77a6-4ca2-ba94-8bb23b2454fc"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8d4dcfcc-f52e-4182-83d6-723d8b74f132"));

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.MSSQL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2345 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<string>(
                name: "AdminNotes",
                table: "Houses",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "ModifiedAt", "ModifiedBy", "Title" },
                values: new object[,]
                {
                    { new Guid("3c3478c3-23d6-44f1-bf03-04a2786a446b"), new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6365), null, null, null, "Denize sıfır, özel havuzlu villalar.", null, null, "Deniz Manzaralı Villalar" },
                    { new Guid("55614a62-c4ec-49f8-970b-6bbe927f312e"), new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6368), null, null, null, "Merkezi konumda modern daireler.", null, null, "Şehir Daireleri" }
                });

            migrationBuilder.InsertData(
                table: "FAQs",
                columns: new[] { "Id", "Answer", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "DisplayOrder", "IsActive", "IsDeleted", "ModifiedAt", "ModifiedBy", "Question" },
                values: new object[,]
                {
                    { new Guid("4df9c6ab-0c21-428b-b313-f9932ce9a525"), "Bəli, evlərimizin 90%-i sürətli internetlə təmin olunub.", new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6321), null, null, null, 1, true, false, null, null, "Evlərdə Wi-Fi var?" },
                    { new Guid("c0d8dc7b-d1fa-4bc0-9e2e-3b4c23801c8c"), "Saytımızdan bəyəndiyiniz evi seçib 'Bron et' düyməsinə basaraq.", new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6335), null, null, null, 2, true, false, null, null, "Necə rezervasiya edə bilərəm?" },
                    { new Guid("dea52882-1765-4b0e-ad84-d67a37cc2d59"), "Giriş 14:00, Çıxış 12:00-dır.", new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6351), null, null, null, 3, true, false, null, null, "Giriş və Çıxış saatları neçədir?" }
                });

            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "IsDeleted", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("27e8f92b-b40d-4d8b-a785-3018bb9ccb0f"), false, "Whatsapp", "https://wa.me/994501234567" },
                    { new Guid("55700970-17b9-40fd-9a45-e7be2101b56e"), false, "Copyright", "© 2025 Güvən Turizm. Bütün hüquqlar qorunur." },
                    { new Guid("5ea1f2d1-8b1c-4d07-94cf-e0e9e9c9fb24"), false, "Address", "H. Əliyev pr., Quba, Azərbaycan" },
                    { new Guid("6d25abb9-ff35-4cb2-abdf-7fdc4f850488"), false, "Facebook", "https://facebook.com/guventurizm" },
                    { new Guid("c0e72497-30bd-41c9-8ac3-b96ddd287b03"), false, "PhoneNumber", "+994 50 123 45 67" },
                    { new Guid("e07701ef-7a58-4766-a442-a8d8e977d197"), false, "Email", "info@guventurizm.az" },
                    { new Guid("f66d7fee-08f6-4ec6-aa7e-0ddef508adae"), false, "Instagram", "https://instagram.com/guventurizm" }
                });

            migrationBuilder.InsertData(
                table: "Houses",
                columns: new[] { "Id", "Address", "AdminNotes", "CategoryId", "City", "CoverImage", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "Description", "Field", "GoogleMapsCode", "ModifiedAt", "ModifiedBy", "NumberOfBeds", "NumberOfFloors", "NumberOfRooms", "Price", "Title" },
                values: new object[,]
                {
                    { new Guid("5c3e9939-2d29-4736-b143-e813dddd1de5"), "Kaş, Antalya", null, new Guid("3c3478c3-23d6-44f1-bf03-04a2786a446b"), "Antalya", "villa1.jpg", new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6374), null, null, null, "3 katlı, 4 odalı, özel havuzlu mükemmel villa.", 350, "https://maps.google.com/...", null, null, (byte)6, (byte)3, (byte)4, 1200.00m, "Kaş’ta Deniz Manzaralı Villa" },
                    { new Guid("80c5bd29-3b77-4192-8413-3d7c4b1d8ef2"), "Şişli, İstanbul", null, new Guid("55614a62-c4ec-49f8-970b-6bbe927f312e"), "İstanbul", "daire1.jpg", new DateTime(2026, 1, 20, 2, 13, 5, 372, DateTimeKind.Utc).AddTicks(6383), null, null, null, "Metroya yakın, 2 odalı şık daire.", 90, "https://maps.google.com/...", null, null, (byte)2, (byte)1, (byte)2, 850.00m, "İstanbul Merkezde Modern Daire" }
                });

            migrationBuilder.InsertData(
                table: "HouseFiles",
                columns: new[] { "Id", "HouseId", "Image" },
                values: new object[,]
                {
                    { new Guid("2373508e-e763-4ef6-921a-92b2e0a99047"), new Guid("80c5bd29-3b77-4192-8413-3d7c4b1d8ef2"), "daire1_1.jpg" },
                    { new Guid("355490f6-c4b0-490a-9c87-042fe02836ca"), new Guid("5c3e9939-2d29-4736-b143-e813dddd1de5"), "villa1_1.jpg" },
                    { new Guid("ec2007e9-0cf9-4e7f-8f97-14c993fb9a1a"), new Guid("5c3e9939-2d29-4736-b143-e813dddd1de5"), "villa1_2.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("4df9c6ab-0c21-428b-b313-f9932ce9a525"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("c0d8dc7b-d1fa-4bc0-9e2e-3b4c23801c8c"));

            migrationBuilder.DeleteData(
                table: "FAQs",
                keyColumn: "Id",
                keyValue: new Guid("dea52882-1765-4b0e-ad84-d67a37cc2d59"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("2373508e-e763-4ef6-921a-92b2e0a99047"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("355490f6-c4b0-490a-9c87-042fe02836ca"));

            migrationBuilder.DeleteData(
                table: "HouseFiles",
                keyColumn: "Id",
                keyValue: new Guid("ec2007e9-0cf9-4e7f-8f97-14c993fb9a1a"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("27e8f92b-b40d-4d8b-a785-3018bb9ccb0f"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("55700970-17b9-40fd-9a45-e7be2101b56e"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("5ea1f2d1-8b1c-4d07-94cf-e0e9e9c9fb24"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("6d25abb9-ff35-4cb2-abdf-7fdc4f850488"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("c0e72497-30bd-41c9-8ac3-b96ddd287b03"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("e07701ef-7a58-4766-a442-a8d8e977d197"));

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: new Guid("f66d7fee-08f6-4ec6-aa7e-0ddef508adae"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("5c3e9939-2d29-4736-b143-e813dddd1de5"));

            migrationBuilder.DeleteData(
                table: "Houses",
                keyColumn: "Id",
                keyValue: new Guid("80c5bd29-3b77-4192-8413-3d7c4b1d8ef2"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3c3478c3-23d6-44f1-bf03-04a2786a446b"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("55614a62-c4ec-49f8-970b-6bbe927f312e"));

            migrationBuilder.DropColumn(
                name: "AdminNotes",
                table: "Houses");

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
        }
    }
}

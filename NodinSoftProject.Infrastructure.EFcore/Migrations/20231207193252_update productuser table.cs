using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NodinSoftProject.Infrastructure.EFcore.Migrations
{
    /// <inheritdoc />
    public partial class updateproductusertable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "25700ddf-f024-4c6c-a874-2f417e256ebd");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "bbe77424-2910-48a9-960a-9d718a54862f");

            migrationBuilder.AddColumn<bool>(
                name: "DeleteAccess",
                table: "ProductUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EditAccess",
                table: "ProductUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "ProductUsers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2bd6a4e8-1268-4822-8cc9-e9024f1ae640", null, "Visitor", "VISITOR" },
                    { "dba935fc-278e-4de0-9eec-f7a91a96d28e", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "2bd6a4e8-1268-4822-8cc9-e9024f1ae640");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "dba935fc-278e-4de0-9eec-f7a91a96d28e");

            migrationBuilder.DropColumn(
                name: "DeleteAccess",
                table: "ProductUsers");

            migrationBuilder.DropColumn(
                name: "EditAccess",
                table: "ProductUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductUsers");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25700ddf-f024-4c6c-a874-2f417e256ebd", null, "Visitor", "VISITOR" },
                    { "bbe77424-2910-48a9-960a-9d718a54862f", null, "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}

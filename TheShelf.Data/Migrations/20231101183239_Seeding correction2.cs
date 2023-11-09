using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheShelf.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seedingcorrection2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "acace79a-546f-493e-9d34-7b2a78f3405e", "AQAAAAIAAYagAAAAEN0YnvX/U3Q3ZxcVQlY60Ncr2coVLMLGdw9wjqVJe4NnP47IrfV03LGgkKkLi9BnCw==", "4dbb8f17-d79e-40a1-b5fa-1080b0789f9a" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e9439c9-9df4-4ab4-8eaa-a3a034d97e6e", "AQAAAAIAAYagAAAAEBl89LjwHDZI42g7gaCHWSIaSGKXTMactuS6s9fqkcRIOke1nkQoi8By9qjrwhalMg==", "36439af7-2f77-409d-a224-f452acd2ef35" });
        }
    }
}

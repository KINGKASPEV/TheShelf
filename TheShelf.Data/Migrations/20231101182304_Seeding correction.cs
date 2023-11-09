using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TheShelf.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seedingcorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9e9439c9-9df4-4ab4-8eaa-a3a034d97e6e", "SUPERADMIN@EXPENDABLE.COM", "AQAAAAIAAYagAAAAEBl89LjwHDZI42g7gaCHWSIaSGKXTMactuS6s9fqkcRIOke1nkQoi8By9qjrwhalMg==", "36439af7-2f77-409d-a224-f452acd2ef35" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "IdentityUser",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7282e14a-36de-4770-81db-dc13d9b41b80", "SUPERADMIN@BEXPENDABLE.COM", "AQAAAAIAAYagAAAAELca52zg7mIbTEU+H/w0oQuSeH0fbTCyIQMTvGcMveYLngX8VRHUBa2e6fhRq8QF5A==", "b26d4e30-8e7f-4c1f-8a49-af71e0663e5b" });
        }
    }
}

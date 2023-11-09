using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TheShelf.Data.Migrations
{
    /// <inheritdoc />
    public partial class Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IdentityUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityUser", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4584b312-1d49-4b92-b3a9-1b6213bd5e59", "4584b312-1d49-4b92-b3a9-1b6213bd5e59", "Admin", "Admin" },
                    { "761b853f-be84-4415-8e2a-54571844b0d8\r\n", "761b853f-be84-4415-8e2a-54571844b0d8\r\n", "User", "User" },
                    { "fe367fc0-8505-467e-a512-3ae0e5fffc06", "fe367fc0-8505-467e-a512-3ae0e5fffc06", "SuperAdmin", "SuperAdmin" }
                });

            migrationBuilder.InsertData(
                table: "IdentityUser",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e", 0, "7282e14a-36de-4770-81db-dc13d9b41b80", "superadmin@expendable.com", false, false, null, "SUPERADMIN@BEXPENDABLE.COM", "SUPERADMIN@EXPENDABLE.COM", "AQAAAAIAAYagAAAAELca52zg7mIbTEU+H/w0oQuSeH0fbTCyIQMTvGcMveYLngX8VRHUBa2e6fhRq8QF5A==", null, false, "b26d4e30-8e7f-4c1f-8a49-af71e0663e5b", false, "superadmin@expendable.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4584b312-1d49-4b92-b3a9-1b6213bd5e59", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" },
                    { "761b853f-be84-4415-8e2a-54571844b0d8\r\n", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" },
                    { "fe367fc0-8505-467e-a512-3ae0e5fffc06", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IdentityUser");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "4584b312-1d49-4b92-b3a9-1b6213bd5e59", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "761b853f-be84-4415-8e2a-54571844b0d8\r\n", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "fe367fc0-8505-467e-a512-3ae0e5fffc06", "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4584b312-1d49-4b92-b3a9-1b6213bd5e59");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "761b853f-be84-4415-8e2a-54571844b0d8\r\n");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fe367fc0-8505-467e-a512-3ae0e5fffc06");
        }
    }
}

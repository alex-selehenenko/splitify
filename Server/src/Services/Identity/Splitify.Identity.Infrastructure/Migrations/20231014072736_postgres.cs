using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password_Hash = table.Column<byte[]>(type: "bytea", nullable: false),
                    Password_Salt = table.Column<byte[]>(type: "bytea", nullable: false),
                    Verified = table.Column<bool>(type: "boolean", nullable: false),
                    VerificationCode_Code = table.Column<string>(type: "text", nullable: false),
                    VerificationCode_CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ResetPasswordToken_Token = table.Column<string>(type: "text", nullable: false),
                    ResetPasswordToken_CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

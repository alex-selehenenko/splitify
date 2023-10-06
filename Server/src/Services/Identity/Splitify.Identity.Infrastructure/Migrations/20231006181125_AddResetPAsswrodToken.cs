using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPAsswrodToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordToken_CreatedAt",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResetPasswordToken_Token",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken_CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ResetPasswordToken_Token",
                table: "Users");
        }
    }
}

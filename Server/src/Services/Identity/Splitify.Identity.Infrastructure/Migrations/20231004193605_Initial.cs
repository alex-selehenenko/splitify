using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password_Hash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Password_Salt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Verified = table.Column<bool>(type: "bit", nullable: false),
                    VerificationCode_Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VerificationCode_CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
